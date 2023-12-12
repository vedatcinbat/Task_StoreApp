using FluentValidation;
using Microsoft.AspNetCore.JsonPatch.Operations;
using StoreApp.API.Data;
using StoreApp.API.Data.DTOs;

namespace StoreApp.API.Validators
{
    public class CategoryFilterSortPaginationValidator : AbstractValidator<CategoryFilterSortPaginationDto>
    {
        public CategoryFilterSortPaginationValidator(StoreAppDbContext _dbContext)
        {
            RuleFor(c => c.Name)
                .Must(IsValidCategoryName).WithMessage("Invalid Category Name")
                .MaximumLength(50).WithMessage("Name can be max 50 characters");

            RuleFor(c => c.Description)
                .MaximumLength(200).WithMessage("Description can be max 200 characters");

            
        }
        private bool IsValidCategoryName(string? categoryName)
        {
            string[] validCategoryNames = { "Electronics", "Foods And Drinks", "Books", "Clothes", null };
            return validCategoryNames.Contains(categoryName);
        }
    }
}
