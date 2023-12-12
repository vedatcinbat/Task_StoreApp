using FluentValidation;
using StoreApp.API.Data.DTOs;

namespace StoreApp.API.Validators
{
    public class ProductFilterSortPaginationValidator : AbstractValidator<ProductFilterSortPaginationDto>
    {
        public ProductFilterSortPaginationValidator()
        {
            // Search
            RuleFor(p => p.search).MaximumLength(50).WithMessage("Search name cannot exceed 50 characters");

            // Filter
            RuleFor(p => p.Id).GreaterThan(0).WithMessage("Id must be greated than zero !");
            RuleFor(p => p.Description).MaximumLength(300).WithMessage("Description can contain max 300 character");
            RuleFor(p => p.MinPrice).GreaterThanOrEqualTo(0).WithMessage("MinPrice must be greater than or equal to zero");
            RuleFor(p => p.MaxPrice).LessThan(1000000).WithMessage("MaxPrice can be maximum 1.000.000");
            RuleFor(p => p.MinQuantity).GreaterThanOrEqualTo(0).WithMessage("Quantity can be minimum 0");
            RuleFor(p => p.MaxQuantity).LessThan(1000000).WithMessage("Quantity can be maximum 1.000.000");
            RuleFor(p => p.CategoryName).MaximumLength(50).WithMessage("Category Name max contain 50 characters");

            // Sort
            RuleFor(p => p.sortby).Must(BeAValidSortBy).WithMessage("Invalid value for sortby");
            RuleFor(dto => dto.sortorder).Must(order => order == "asc" || order == "desc" || order == null).WithMessage("Invalid value for sortorder");

            // Pagination
            RuleFor(p => p.PageNumber).GreaterThan(0).WithMessage("Page Number must greater than 0");
            RuleFor(p => p.PageSize).GreaterThan(0).WithMessage("Page Size must greater than 0").LessThan(1000).WithMessage("Page Size must less than 1000");
        }
        private bool BeAValidSortBy(string sortby)
        {
            string[] allowedSortByValues = { "id", "name", "price", "quantity", null };
            return allowedSortByValues.Contains(sortby, StringComparer.OrdinalIgnoreCase);
        }
    }
}
