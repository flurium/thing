using Thing.Models;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bll.Services;
using Bll.Models;

namespace Thing.Controllers
{
  [Authorize(Roles = Roles.Admin)]
  public class AdminController : Controller
  {
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly BanService _banService;
    private readonly ProductService _productService;

    public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, BanService banService, ProductService productService)
    {
      _roleManager = roleManager;
      _userManager = userManager;
      _banService = banService;
      _productService = productService;
    }

    [Authorize(Roles = "")]
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

    public async Task<IActionResult> Users(UserViewModel filter)
    {
      var users = await _banService.FilterUsers(new UserFilter
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
          IsBanned = await _userManager.IsInRoleAsync(user, Roles.Banned),
          IsAdmin = await _userManager.IsInRoleAsync(user, Roles.Admin)
        });
      }

      ViewBag.Url = Request.Path + Request.QueryString;

      return View(userViewModels);
    }

    public async Task<IActionResult> MakeAdmin(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      await _userManager.AddToRoleAsync(user, Roles.Admin);
      return RedirectToAction(nameof(Users));
    }

    public async Task<IActionResult> UnmakeAdmin(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      await _userManager.RemoveFromRoleAsync(user, Roles.Admin);
      return RedirectToAction(nameof(Users));
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