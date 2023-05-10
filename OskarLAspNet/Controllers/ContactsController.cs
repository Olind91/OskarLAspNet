using Microsoft.AspNetCore.Mvc;

namespace OskarLAspNet.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
