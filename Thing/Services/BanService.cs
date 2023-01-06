using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Thing.Models;
using Thing.Repository;

namespace Thing.Services
{
    public class BanService
    {
        private readonly AnswerRepository _answerRepository;
        private readonly ProductRepository _productRepository;
        private readonly CommentRepository _commenttRepository;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public BanService(AnswerRepository answerRepository, UserManager<User> userManager, IEmailSender emailSender,
            ProductRepository productRepository, CommentRepository commenttRepository)
        {
            _answerRepository = answerRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _productRepository = productRepository;
            _commenttRepository = commenttRepository;
        }

        public async Task UserAsync(string uid)
        {
            var user = await _userManager.FindByIdAsync(uid);
            var res = await _userManager.AddToRoleAsync(user, Roles.Banned);
            if (res.Succeeded)
            {
                await _emailSender.SendEmailAsync(user.Email, "Banned in Thing",
                    "YOU ARE BANNED IN THING! You did bad actions. Reply to this email with apologies to get unbanned!");
            }
        }

        public async Task ProductAsync(int id)
        {
            var product = await _productRepository.DeleteAndReturn(id);
            if (product != null)
            {
                var user = await _userManager.FindByIdAsync(product.SellerId);
                await _emailSender.SendEmailAsync(user.Email, "Product got banned", $"Your product {product.Name} got banned.");
            }
        }

        public async Task CommentAsync(int id)
        {
            var comment = await _commenttRepository.DeleteAndReturn(id);
            if (comment != null)
            {
                var user = await _userManager.FindByIdAsync(comment.UserId);
                await _emailSender.SendEmailAsync(user.Email, "Comment got banned", $"Your comment for {comment.Product.Name} got banned.");
            }
        }

        public async Task AnswerAsync(int id)
        {
            await _answerRepository.Delete(id);
            // sending email doesn't mean
        }
    }
}