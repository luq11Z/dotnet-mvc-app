using FluentValidation;
using FluentValidation.Results;
using LStudies.Business.Interfaces;
using LStudies.Business.Models;
using LStudies.Business.Notifications;

namespace LStudies.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        // Get error messages if there are any and notify
        protected void Notify(ValidationResult validatioResult)
        {
            foreach (var error in validatioResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string errorMessage)
        {
            _notifier.Handle(new Notification(errorMessage));
        }

                                                                                                            // and TE is an entity
        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
            {
                return true;
            }

            Notify(validator);

            return false;
        }
    }
}
