using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thing.Models;

namespace Thing.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roleExists = await _roleManager.RoleExistsAsync(Roles.Admin);

            bool adminExists = false;

            if (roleExists)
            {
                var admins = await _userManager.GetUsersInRoleAsync(Roles.Admin);
                adminExists = admins.Count > 0;
            }
            else
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (adminExists) return View("Error");

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var res = await _userManager.AddToRoleAsync(user, Roles.Admin);

            return res.Succeeded ? RedirectToAction(nameof(Index)) : View("Error");
        }
    }
}