using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using StoreApp.API.Data.DTOs;

namespace StoreApp.API.Validators
{
    public class JsonPatchValidator : AbstractValidator<JsonPatchDocument<UpdateProductRequestDto>>
    {
        public JsonPatchValidator()
        {
            RuleForEach(x => x.Operations)
                .Must(BeAValidOperation).WithMessage("Path can be only Name - Description - Price - Quantity - CategoryId - IsDeleted");
        }

        private bool BeAValidOperation(Operation<UpdateProductRequestDto> operation)
        {
            string[] validPaths = { "name", "description", "price", "quantity", "categoryid", "isdeleted" };
            return validPaths.Contains(operation.path.ToLower());
        }
    }
}
