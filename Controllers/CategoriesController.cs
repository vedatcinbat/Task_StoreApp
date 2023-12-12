using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoreApp.API.Data;
using StoreApp.API.Data.DTOs;
using StoreApp.API.Data.Entities;
using StoreApp.API.Validators;

namespace StoreApp.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private IMapper mapper;
        private StoreAppDbContext _dbContext;
        private readonly IValidator<CategoryFilterSortPaginationDto> _categoryFilterSortPaginationValidator;
        private readonly IValidator<AddCategoryRequestedDto> _createCatoryValidator;

        public CategoriesController(
            IMapper mapper, 
            StoreAppDbContext dbContext,
            IValidator<CategoryFilterSortPaginationDto> categoryFilterSortPaginationValidator,
            IValidator<AddCategoryRequestedDto> createCatoryValidator
            )
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
            this._categoryFilterSortPaginationValidator = categoryFilterSortPaginationValidator;
            _createCatoryValidator = createCatoryValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryFilterSortPaginationDto? categoryFSPDto)
        {
            var categoryFSPValidationResult = _categoryFilterSortPaginationValidator.Validate(categoryFSPDto);

            if (categoryFSPValidationResult == null)
            {
                return BadRequest(categoryFSPValidationResult.Errors);
            }

            // If its validated
            //var query = _dbContext.Categories.Include("Products").AsQueryable();
            var query = _dbContext.Categories.Include(c => c.Products.Where(p => !p.IsDeleted)).Where(c => c.IsDeleted == false).AsQueryable();
  

            // Filter
            if(categoryFSPDto.Name != null)
            {
                query = query.Where(c => c.Name == categoryFSPDto.Name);
            }
            if(categoryFSPDto.Description != null)
            {
                query = query.Where(c => c.Description.ToLower().Contains(categoryFSPDto.Description.ToLower()));
            }
            
            

            var categories = query.ToList();


            return Ok(mapper.Map<List<CategoryDto>>(categories));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneCategoryById([FromRoute] int id)
        {
            if(id < 0)
            {
                return BadRequest("Id can be min zero");
            }

            var category = await _dbContext.Categories.Include("Products").FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return NotFound($"Category coudn't found with id {id}");
            }

            var categoryDto = mapper.Map<CategoryDto>(category);
            // 200 OK
            return Ok(categoryDto);

        }

        [HttpPost]
        public IActionResult CreateCategory(AddCategoryRequestedDto categoryRequestedDto)
        {
            // Validate that categoryRequestedDto
            var validationResult = _createCatoryValidator.Validate(categoryRequestedDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var categoryDomain = mapper.Map<Category>(categoryRequestedDto);


            _dbContext.Categories.Add(categoryDomain);
            _dbContext.SaveChanges();

            return Ok(mapper.Map<CategoryDto>(categoryDomain));

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return NotFound($"Category coudnt found in id : {id}");
            }

            category.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return Ok($"Category with id {id} has been deleted");


        }

        
    }
}
