using Microsoft.AspNetCore.Mvc;

namespace OskarLAspNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
