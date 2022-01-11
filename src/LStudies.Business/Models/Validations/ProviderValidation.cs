using FluentValidation;
using LStudies.Business.Models.Validations.Documents;

namespace LStudies.Business.Models.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(p => p.Name)
                // Set custom messages
                //.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                .NotEmpty()
                .Length(2, 100); //.WithMessage("Custom message")

            // Work with special cases, we can choose what to do
            When(p => p.ProviderType == ProviderType.PrivateIndividual, () => 
            {
                RuleFor(p => p.Document.Length).Equal(CpfValidation.CpfSize)
                    .WithMessage("Document must be {ComparisonValue} characters, {PropertyValue} provided");

                RuleFor(p => CpfValidation.Validate(p.Document)).Equal(true)
                    .WithMessage("Provided Document is not valid");
            });

            When(p => p.ProviderType == ProviderType.LegalEntity, () => 
            {
                RuleFor(p => p.Document.Length).Equal(CnpjValidation.CnpjSize)
                   .WithMessage("Document must be {ComparisonValue} characters, {PropertyValue} provided");

                RuleFor(p => CnpjValidation.Validate(p.Document)).Equal(true)
                    .WithMessage("Provided Document is not valid");
            });
        }
    }
}
