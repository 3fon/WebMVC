using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
