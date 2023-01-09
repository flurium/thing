using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Thing.Models;
using Thing.Services;

namespace Thing.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly ImageService _imageService;
        private readonly IWebHostEnvironment _appEnvironment;

        public CatalogController(CatalogService catalogService, ImageService imageService, IWebHostEnvironment appEnvironment)
        {
            _catalogService = catalogService;
            _imageService = imageService;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCategoriesAsync());
        }

        public async Task<IActionResult> Products(int id)
        {
            ViewBag.CategoryId = id;
            return View(await _catalogService.GetProductsForCategoryAsync(id));
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.ProductId = id;
            var product = await _catalogService.GetProductDetailsAsync(id);
            if (product == null) return View("Error");
            return View(product);
        }

        public async Task<IActionResult> Comments(int id)
        {
            ViewBag.Url = Request.Path + Request.QueryString;
            ViewBag.ProductId = id;
            var comments = await _catalogService.GetCommentsWithAnswersAsync(id);
            return View(comments);
        }

        [Authorize]
        public async Task<IActionResult> WriteComment(Comment comment, int ProductId, IFormFileCollection uploads)
        {
            comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _catalogService.AddCommentAsync(comment);

            if (string.IsNullOrWhiteSpace(_appEnvironment.WebRootPath))
            {
                _appEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            foreach (var image in uploads)
            {
                if (image.Name != null && image.Name != "")
                {
                    string path = "/Images/" + image.FileName;
                    string alpath = _appEnvironment.WebRootPath + path;
                    using (var fileStream = new FileStream(alpath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    CommentImage commentImage = new() { ImagePath = path, CommentId = comment.Id };
                    await _imageService.AddCommentImageAsync(commentImage);
                }
            }

            return RedirectToAction(nameof(Comments), new { ProductId });
        }

        [Authorize]
        public async Task<IActionResult> ToFavorites(int ProductId, int CategoryId)
        {
            var favorite = new Favorite()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ProductId = ProductId
            };
            if (await _catalogService.IsFavoriteExistsAsync(favorite)) return RedirectToAction(nameof(Details), new { id = ProductId });

            await _catalogService.AddFavoriteAsync(favorite);
            return RedirectToAction(nameof(Products), new { id = CategoryId });
        }

        [Authorize]
        public IActionResult WriteAnswer(int CommentId, int ProductId)
        {
            ViewBag.CommentId = CommentId;
            ViewBag.ProductId = ProductId;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> PostAnswer(Answer Answer, int ProductId, int CategoryId)
        {
            Answer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _catalogService.AddAnswerAsync(Answer);
            return RedirectToAction(nameof(Comments), new { id = ProductId });
        }

        [Authorize]
        public async Task<IActionResult> ToCart(int ProductId, int CategoryId)
        {
            var order = new Order()
            {
                Count = 1,
                ProductId = ProductId,
                State = "INCART",
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            if (await _catalogService.IsOrderExistsAsync(order)) return RedirectToAction(nameof(Details), new { id = ProductId });

            await _catalogService.AddOrderAsync(order);

            return RedirectToAction(nameof(Products), new { id = CategoryId });
        }
    }
}