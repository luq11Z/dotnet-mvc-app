using FluentValidation;

namespace LStudies.Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.PublicPlace)
                .NotEmpty().WithMessage("{PropertyName} is required}")
                .Length(2, 200).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(a => a.District)
                .NotEmpty().WithMessage("{PropertyName} is required}")
                .Length(2, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(a => a.PostalCode)
              .NotEmpty().WithMessage("{PropertyName} is required}")
              .Length(8).WithMessage("{PropertyName} must be {MaxLength} characters");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("{PropertyName} is required}")
                .Length(2, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(a => a.State)
               .NotEmpty().WithMessage("{PropertyName} is required}")
               .Length(2, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(a => a.Number)
               .NotEmpty().WithMessage("{PropertyName} is required}")
               .Length(1, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
        }
    }
}
