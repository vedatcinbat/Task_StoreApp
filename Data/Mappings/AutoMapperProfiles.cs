using AutoMapper;
using StoreApp.API.Data.DTOs;
using StoreApp.API.Data.Entities;

namespace StoreApp.API.Data.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, AddProductRequestedDto>().ReverseMap();
            CreateMap<Product, UpdateProductRequestDto>().ReverseMap();
            CreateMap<ProductDto, UpdateProductRequestDto>().ReverseMap();
            CreateMap<Product, CategoryProductDto>().ReverseMap();

            CreateMap<Category, ProductCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, AddCategoryRequestedDto>().ReverseMap();
            CreateMap<CategoryDto, AddCategoryRequestedDto>().ReverseMap();
            CreateMap<Category, CategorySimpleDto>().ReverseMap();
            CreateMap<Category, CategoryFilterSortPaginationDto>().ReverseMap(); 
            CreateMap<CategoryDto, CategoryFilterSortPaginationDto>().ReverseMap();


            CreateMap<CategoryDto, CategorySimpleDto>().ReverseMap();
        }
    }
}
