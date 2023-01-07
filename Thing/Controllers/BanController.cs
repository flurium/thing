using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Thing.Models;
using Thing.Models.ViewModels;
using Thing.Services;

namespace Thing.Controllers
{
    // To ban different things
    [Authorize(Roles = Roles.Admin)]
    public class BanController : Controller
    {
        private readonly BanService _banService;
        private readonly ProductService _productService;
        private readonly UserManager<User> _userManager;

        public BanController(BanService banService, UserManager<User> userManager, ProductService productService)
        {
            _banService = banService;
            _userManager = userManager;
            _productService = productService;
        }

        public async Task<IActionResult> Users(UserViewModel filter)
        {
            var users = await _banService.FilterUsers(new UserFilterViewModel
            {
                Email = filter.Email ?? "",
                Id = filter.Id ?? "",
                Name = filter.Name ?? ""
            });

            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.UserName,
                    IsBanned = await _userManager.IsInRoleAsync(user, Roles.Banned)
                });
            }

            ViewBag.Url = Request.Path + Request.QueryString;

            return View(userViewModels);
        }

        public async Task<IActionResult> UnbanUser(string id, string redirect)
        {
            await _banService.UnbanUser(id);
            return Redirect(redirect);
        }

        public async Task<IActionResult> BanUser(string id, string redirect)
        {
            await _banService.BanUser(id);
            return Redirect(redirect);
        }

        public async Task<IActionResult> Products(BanProductFilter filter)
        {
            ViewBag.Url = Request.Path + Request.QueryString;
            return View(await _productService.Filter(filter));
        }

        // Will be used from regular views
        public async Task<IActionResult> Product(int id, string redirect)
        {
            await _banService.BanProduct(id);
            return Redirect(redirect);
        }

        // Will be used from regular views
        public async Task<IActionResult> Comment(int id, string redirect)
        {
            await _banService.BanComment(id);
            return Redirect(redirect);
        }

        // Will be used from regular views
        public async Task<IActionResult> Answer(int id, string redirect)
        {
            await _banService.BanAnswer(id);
            return Redirect(redirect);
        }
    }
}