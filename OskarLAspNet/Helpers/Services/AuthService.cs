using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OskarLAspNet.Models.Identity;
using OskarLAspNet.Models.ViewModels;
using System.Linq.Expressions;

namespace OskarLAspNet.Helpers.Services
{
    public class AuthService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly AddressService _addressService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, AddressService addressService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _addressService = addressService;
            _signInManager = signInManager;
        }



        public async Task<bool> UserAlreadyExistsAsync(Expression<Func<AppUser, bool>> expression)
        {
            return await _userManager.Users.AnyAsync(expression);

        }


        public async Task<bool> RegisterUserAsync(UserRegisterVM viewModel)
        {
            AppUser appUser = viewModel;

            var result = await _userManager.CreateAsync(appUser, viewModel.Password);
            if (result.Succeeded)
            {
                //2.16.00 ish föreläsning 7.
                var addressEntity = await _addressService.GetOrCreateAsync(viewModel);
                if (addressEntity != null)
                {
                    await _addressService.AddAddressAsync(appUser, addressEntity);
                    return true;
                }
            }
            return false;

        }

        //Inlogg
        public async Task<bool> LoginAsync(UserLoginVM viewModel)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == viewModel.Email);
            if (appUser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(appUser, viewModel.Password, viewModel.RememberMe, false);
                return result.Succeeded;
            }

            return false;
        }
    }
}
