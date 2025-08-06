using Microsoft.AspNetCore.Mvc;

namespace Intellimen.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
