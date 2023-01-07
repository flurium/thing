using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thing.Models;
using Thing.Models.ViewModels;
using Thing.Services;

namespace Thing.Controllers
{
    // To ban different things

    public class BanController : Controller
    {
        private BanService _banService;
        private readonly UserManager<User> _userManager;

        public BanController(BanService banService, UserManager<User> userManager)
        {
            _banService = banService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();

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

            return View(userViewModels);
        }

        public async Task<IActionResult> UnbanUser(string id)
        {
            await _banService.UnbanUser(id);
            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> BanUser(string id)
        {
            await _banService.BanUser(id);
            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> Product(int id, string redirect)
        {
            await _banService.BanProduct(id);
            return Redirect(redirect);
        }

        public async Task<IActionResult> Answer(int id, string redirect)
        {
            await _banService.BanAnswer(id);
            return Redirect(redirect);
        }
    }
}