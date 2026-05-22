using FluentValidation;
using ProductMicroService.Core.DTO;

namespace ProductMicroService.Core.Validator
{

    /// <summary>
    /// ProductAddRequestValidator is a class that inherits from AbstractValidator<ProductAddRequest> and defines validation rules for the ProductAddRequest DTO. It ensures that the product name is not empty, the category is a valid enum value, the unit price is non-negative, and the quantity in stock is non-negative. This helps maintain data integrity and provides meaningful error messages when validation fails.
    /// </summary>
    public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidator()
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
