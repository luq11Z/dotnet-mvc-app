using LStudies.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LStudies.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Title = "An error has occurred!";
                modelError.Message = "An error has occurred! Try again later or contact our support";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Title = "Ops! Page not found.";
                modelError.Message = "The page you are looking for doesn't exist!";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Title = "Permission denied!";
                modelError.Message = "You do not have permissios to do this action.";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelError);
        }
    }
}
