using LStudies.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LStudies.App.Controllers
{
    /* Makes controllers cleaner. */
    public abstract class BaseController : Controller
    {
        private readonly INotifier _notifier;

        public BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        /* If there are no notifications it means there are no errors*/
        protected bool IsOperationValid()
        {
            return !_notifier.HasNotification();
        }
    }
}
