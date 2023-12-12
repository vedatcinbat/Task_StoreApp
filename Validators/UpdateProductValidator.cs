using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StoreApp.API.Data;
using StoreApp.API.Data.DTOs;

namespace StoreApp.API.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequestDto>
    {
        private readonly StoreAppDbContext _dbContext;
        public UpdateProductValidator(StoreAppDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(p => p.Name)
                .MaximumLength(30)
                .WithMessage("Name can be max 30 character");

            RuleFor(p => p.Description)
                .MaximumLength(50)
                .WithMessage("Description can be max 50 character");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price can be greater or equal to zero");

            RuleFor(p => p.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity can be greater or equal to zero");

            RuleFor(p => p.CategoryId)
                .Must(BeValidCategoryId).WithMessage("Invalid category ID"); ;

        }
        private bool BeValidCategoryId(int? categoryId)
        {
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                var categoryExists = _dbContext.Categories.Any(c => c.Id == categoryId.Value);
                return categoryExists;
            }
            return false;
        }
    }
}
