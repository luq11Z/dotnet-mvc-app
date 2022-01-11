using FluentValidation;

namespace LStudies.Business.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(2, 200).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(2, 1000).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");
        }
    }
}
