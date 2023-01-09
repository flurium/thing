using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Thing.Models;
using Thing.Models.ViewModels;
using Thing.Services;

namespace Thing.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SellerService _sellerService;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager, SellerService sellerService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _sellerService = sellerService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            var res = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);
            if (res.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View("Error");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new User()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Name,
            };

            var res = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (res.Succeeded)
            {
                if (registerViewModel.IsSeller)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(Roles.Seller);

                    if (!roleExists) await _roleManager.CreateAsync(new IdentityRole(Roles.Seller));

                    await _userManager.AddToRoleAsync(user, Roles.Seller);
                    await _sellerService.CreateAsync(new Seller
                    {
                        Id = user.Id,
                        About = "about"
                    });
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("", "confirmation", new { guid = token, userEmail = user.Email }, Request.Scheme, Request.Host.Value);
                await _emailSender.SendEmailAsync(user.Email, "ConfirmationLink", $"Link-> <a href={confirmationLink}>Confirmation Link</a>");
                ViewBag.Email = user.Email;
                return View("Confirmation");
            }

            return View("Error");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}