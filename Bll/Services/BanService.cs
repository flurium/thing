using Bll.Models;
using Dal.UnitOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bll.Services
{
  public class BanService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailSender _emailSender;

    public BanService(IUnitOfWork unitOfWork, UserManager<User> userManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
    {
      _unitOfWork = unitOfWork;
      _userManager = userManager;
      _emailSender = emailSender;
      _roleManager = roleManager;
    }

    public async Task<IEnumerable<User>> FilterUsers(UserFilter filter)
    {
      List<Expression<Func<User, bool>>> predicates = new();

      if (filter.Name != "") predicates.Add(u => u.UserName.StartsWith(filter.Name));
      if (filter.Email != "") predicates.Add(u => u.Email.StartsWith(filter.Email));
      if (filter.Id != "") predicates.Add(u => u.Id.StartsWith(filter.Id));

      var users = _userManager.Users;

      foreach (var predicate in predicates)
      {
        users = users.Where(predicate);
      }

      return await users.ToListAsync();
    }

    public async Task UnbanUser(string uid)
    {
      var user = await _userManager.FindByIdAsync(uid);
      var res = await _userManager.RemoveFromRoleAsync(user, Roles.Banned);
      if (res.Succeeded)
      {
        await _emailSender.SendEmailAsync(user.Email, "Unbanned in Thing", "You were unbanned in Thing!");
      }
    }

    public async Task BanUser(string uid)
    {
      var user = await _userManager.FindByIdAsync(uid);
      if (!await _roleManager.RoleExistsAsync(Roles.Banned)) await _roleManager.CreateAsync(new IdentityRole(Roles.Banned));

      var res = await _userManager.AddToRoleAsync(user, Roles.Banned);
      if (res.Succeeded)
      {
        await _emailSender.SendEmailAsync(user.Email, "Banned in Thing",
            "YOU ARE BANNED IN THING! You did bad actions. Reply to this email with apologies to get unbanned!");
      }
    }

    public async Task BanProduct(int id)
    {
      var product = await _unitOfWork.ProductRepository.DeleteAndReturn(id);
      if (product != null)
      {
        var user = await _userManager.FindByIdAsync(product.SellerId);
        await _emailSender.SendEmailAsync(user.Email, "Product got banned", $"Your product {product.Name} got banned.");
      }
    }

    public async Task BanComment(int id)
    {
      var comment = await _unitOfWork.CommentRepository.DeleteAndReturn(id);
      if (comment != null)
      {
        var user = await _userManager.FindByIdAsync(comment.UserId);
        await _emailSender.SendEmailAsync(user.Email, "Comment got banned", $"Your comment for {comment.Product.Name} got banned.");
      }
    }

    public async Task BanAnswer(int id)
    {
      // sending email doesn't mean
      await _unitOfWork.AnswerRepository.Delete(id);
    }
  }
}