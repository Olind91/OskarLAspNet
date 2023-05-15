using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OskarLAspNet.Helpers.Services;
using OskarLAspNet.Models.ViewModels;

namespace OskarLAspNet.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }




        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(ProductRegVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.CreateProductAsync(viewModel);
                if (product != null)
                {
                    if (viewModel.Image != null)
                        await _productService.UploadImageAsync(product, viewModel.Image);
                    // await _productService.CreateProductAsync(viewModel);
                    return RedirectToAction("/");

                }

                ModelState.AddModelError("", "Something Went Wrong.");
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
    }
}
