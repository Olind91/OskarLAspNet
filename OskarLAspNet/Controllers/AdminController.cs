using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OskarLAspNet.Contexts;
using OskarLAspNet.Helpers.Services;
using OskarLAspNet.Models.Dtos;
using OskarLAspNet.Models.Identity;

namespace OskarLAspNet.Controllers
{

    public class AdminController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ProductService _productService;

        public AdminController(DataContext dataContext, ProductService productService)
        {
            _dataContext = dataContext;
            _productService = productService;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        public IActionResult GetAllUsers()
        {
            List<AppUser> userList = _dataContext.Users.ToList();
            return View(userList);
            
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminProducts()
        {

            var products = await _productService.GetAllAsync();
            return View(products);

        }

    }
}
