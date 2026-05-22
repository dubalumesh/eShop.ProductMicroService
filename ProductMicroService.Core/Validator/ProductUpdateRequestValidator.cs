using FluentValidation;
using ProductMicroService.Core.DTO;
using ProductMicroService.Core.Entities;

namespace ProductMicroService.Core.Validator
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator()
        {
            //ProductName
            RuleFor(temp => temp.ProductName)
              .NotEmpty().WithMessage("Product Name can't be blank");

            //UnitPrice
            RuleFor(temp => temp.UnitPrice)
              .InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price should between 0 to {double.MaxValue}");

            //QuantityInStock
            RuleFor(temp => temp.QuantityInStock)
              .InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in Stock should between 0 to {int.MaxValue}");

        }
    }
}
