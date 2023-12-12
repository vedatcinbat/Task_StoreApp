using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StoreApp.API.Data;
using StoreApp.API.Data.DTOs;
using StoreApp.API.Data.Entities;
using StoreApp.API.Validators;
using System.Globalization;
using System.Linq;

namespace StoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreAppDbContext _dbContext;
        private readonly IMapper mapper;
        private readonly IValidator<ProductFilterSortPaginationDto> _productFilterSortPaginationValidator;
        private readonly IValidator<JsonPatchDocument<UpdateProductRequestDto>> _jsonPatchValidator;
        private readonly IValidator<UpdateProductRequestDto> _updateProductValidator;
        private readonly IValidator<AddProductRequestedDto> _createProductValidator;

        public ProductsController(
            IMapper mapper,
            StoreAppDbContext dbContext,
            IValidator<AddProductRequestedDto> createProductValidator,
            IValidator<ProductFilterSortPaginationDto> producFilterSortPaginationValidator,
            IValidator<UpdateProductRequestDto> updateProductValidator,
            IValidator<JsonPatchDocument<UpdateProductRequestDto>> jsonPatchValidator
            )
        {
         
            this._productFilterSortPaginationValidator = producFilterSortPaginationValidator;
            this._jsonPatchValidator = jsonPatchValidator;
            this._updateProductValidator = updateProductValidator;
            this._createProductValidator = createProductValidator;
            this._dbContext = dbContext;
            this.mapper = mapper;
        }


        // Get All Products
        // GET: https://localhost:port/api/products?search=p1-p2-p3&filterDto[id,description]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] ProductFilterSortPaginationDto productFSPDto
            )
        {
            // Validation Proccess
            var validationResult = _productFilterSortPaginationValidator.Validate(productFSPDto);
            if (validationResult == null)
            {
                return BadRequest(validationResult.Errors);
            }
            

            var query = _dbContext.Products.Include("Category").Where(p => p.Category.IsDeleted == false).Where(p => p.IsDeleted == false).AsQueryable();

            // Search

            if(productFSPDto.search != null)
            {
                bool searchContainLine = productFSPDto.search.Contains("-");
                if (searchContainLine)
                {
                    var terms = productFSPDto.search.Split("-");
                    if (terms.Length == 1)
                    {
                        query = query.Where(p => p.Name.ToLower().Contains(terms[0].ToLower()));

                    }
                    else if (terms.Length == 2)
                    {
                        query = query.Where(p => p.Name.ToLower().Contains(terms[0].ToLower()) || p.Name.ToLower().Contains(terms[1].ToLower()));
                    }
                    else if (terms.Length == 3)
                    {
                        query = query.Where(p => p.Name.ToLower().Contains(terms[0].ToLower()) || p.Name.ToLower().Contains(terms[1].ToLower()) || p.Name.ToLower().Contains(terms[2].ToLower()));
                    }
                }else
                {
                    query = query.Where(p => p.Name.ToLower().Contains(productFSPDto.search.ToLower()));
                }
            }
            
                


            // Filters

            if (productFSPDto.Id != null)
            {
                query = query.Where(p => p.Id == productFSPDto.Id);
            }
            if (productFSPDto.Description != null)
            {
                query = query.Where(p => p.Description.ToLower().Contains(productFSPDto.Description.ToLower()));
            }
            if (productFSPDto.MinPrice != null)
            {
                query = query.Where(p => p.Price >= Convert.ToDouble(productFSPDto.MinPrice));
            }
            if (productFSPDto.MaxPrice != null)
            {
                query = query.Where(p => p.Price <= Convert.ToDouble(productFSPDto.MaxPrice));
            }
            if (productFSPDto.MinQuantity != null)
            {
                query = query.Where(p => p.Quantity >= productFSPDto.MinQuantity);
            }
            if (productFSPDto.MaxQuantity != null)
            {
                query = query.Where(p => p.Quantity <= productFSPDto.MaxQuantity);
            }
            if(productFSPDto.CategoryName != null)
            {
                // Check if CategoryName contains "-"
                bool categoryNameIncludeDash = productFSPDto.CategoryName.Contains("-");
                if(!categoryNameIncludeDash)
                {
                    query = query.Where(p => p.Category.Name == productFSPDto.CategoryName);
                }else
                {
                    // Split "-"
                    var terms = productFSPDto.CategoryName.Split("-");
                    if(terms.Length == 2)
                    {
                        query = query.Where(p => p.Category.Name.ToLower().Contains(terms[0].ToLower()) || p.Category.Name.ToLower().Contains(terms[1].ToLower()));
                    }else if(terms.Length == 3)
                    {
                        query = query.Where(p => p.Category.Name.ToLower().Contains(terms[0].ToLower()) || p.Category.Name.ToLower().Contains(terms[1].ToLower()) || p.Category.Name.ToLower().Contains(terms[2].ToLower()));
                    }
                }
                
            }


            // sorting

            if (!string.IsNullOrEmpty(productFSPDto.sortby))
            {

                switch (productFSPDto.sortby.ToLower())
                {
                    case "id":
                        query = (productFSPDto.sortorder == "desc") ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
                        break;
                    case "name":
                        query = (productFSPDto.sortorder == "desc") ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    case "quantity":
                        query = (productFSPDto.sortorder == "desc") ? query.OrderByDescending(p => p.Quantity) : query.OrderBy(p => p.Quantity);
                        break;
                    case "price":
                        query = (productFSPDto.sortorder == "desc") ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                        break;

                }

            }else if((productFSPDto.sortorder != null && productFSPDto.sortby == null) || (productFSPDto.sortorder == null && productFSPDto.sortby != null))
            {
                return BadRequest("Missing order or sortby");
            }

            // pagination

            if(productFSPDto.PageNumber == null && productFSPDto.PageSize == null)
            {
                productFSPDto.PageNumber = 1;
                productFSPDto.PageSize = 100;
            }

            int pageSize = Convert.ToInt32(productFSPDto.PageSize);
            int pageNumber = Convert.ToInt32(productFSPDto.PageNumber) - 1; 

            query = query.Skip(pageNumber * pageSize).Take(pageSize);

            return Ok(mapper.Map<List<ProductDto>>(query));

            
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _dbContext.Products.Include("Category").FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                return BadRequest("Id is not valid");
            }
            // Product --> DTO
            return Ok(mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProductRequestedDto addProductRequestedDto)
        {
            // Validation
            var validationResult = _createProductValidator.Validate(addProductRequestedDto);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var product = mapper.Map<Product>(addProductRequestedDto);

            // After Validation
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            
            return Ok(mapper.Map<ProductDto>(product));
            
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct(
            [FromRoute] int? id,
            [FromBody] JsonPatchDocument<UpdateProductRequestDto> patchDoc
            )
        {
            var validationResult = _jsonPatchValidator.Validate(patchDoc);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            if (patchDoc == null)
            {
                return BadRequest();
            }

            var product = await _dbContext.Products.Include("Category").FirstAsync(p => p.Id == id);

            if ( product == null )
            {
                return BadRequest("Product coudnt found");
            }

            var productToPatch = mapper.Map<UpdateProductRequestDto>(product);

            patchDoc.ApplyTo(productToPatch, ModelState);

            mapper.Map(productToPatch, product);

            await _dbContext.SaveChangesAsync();

            // Id:0 and Category field is not resolving
            return Ok(mapper.Map<ProductDto>(productToPatch));


        }
        
        // Soft Delete
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await _dbContext.Products.FindAsync(id);
            if(deletedProduct == null)
            {
                return NotFound($"Product coudnt found. Invalid id {id}");
            }

            deletedProduct.IsDeleted = true;
            await _dbContext.SaveChangesAsync();


            return Ok(mapper.Map<ProductDto>(deletedProduct));
        }
    }
}
