using FluentValidation;
using StoreApp.API.Data.DTOs;

namespace StoreApp.API.Validators
{
    public class CreateCategoryValidator : AbstractValidator<AddCategoryRequestedDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name).MaximumLength(50).WithMessage("Category name length can be max 50 characters");
            RuleFor(c => c.Description).MaximumLength(150).WithMessage("Category description length can be max 50 characters");           
        }
    }
}
