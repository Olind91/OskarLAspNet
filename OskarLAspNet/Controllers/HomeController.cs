using Microsoft.AspNetCore.Mvc;
using OskarLAspNet.Helpers.Services;

namespace OskarLAspNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;

        public HomeController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);

        }
    }
}
