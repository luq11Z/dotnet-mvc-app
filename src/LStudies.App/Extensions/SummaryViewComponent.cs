using LStudies.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LStudies.App.Extensions
{
    /* This class will be a view to summarize potential validation errors  */
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifier _notifier;

        public SummaryViewComponent(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            /* GetNotifications is not async, thas why Task.FromResult */
            var notifications = await Task.FromResult(_notifier.GetNotifications());

            /* Insert error messages into the model state as model error, so it can be treated as a field error  */
            notifications.ForEach(n => ViewData.ModelState.AddModelError(string.Empty, n.Message));

            return View();
        }
    }
}
