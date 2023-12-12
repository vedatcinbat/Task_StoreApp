using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StoreApp.API.Data;
using StoreApp.API.Data.DTOs;
using StoreApp.API.Data.Entities;

namespace StoreApp.API.Validators
{
    public class CreateProductValidator : AbstractValidator<AddProductRequestedDto>
    {
        private readonly StoreAppDbContext _dbContext;
        

        public CreateProductValidator(StoreAppDbContext dbContext)
        {
            this._dbContext = dbContext;
            // Validators
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required !")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater then zero");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Product quantity must be greater than or equal to zero");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId have to greater then zero !")
                .NotNull().WithMessage("Category is required")
                .Must(BeValidCategoryId).WithMessage("Invalid category ID");
            
                
        }
        private bool BeValidCategoryId(int? categoryId)
        {
            if(categoryId.HasValue && categoryId.Value > 0)
            {
                var categoryExists = _dbContext.Categories.Any(c => c.Id == categoryId.Value);
                return categoryExists;
            }
            return false;
        }
    }
}
