using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Security.Claims;
using Thing.Models;
using Thing.Models.ViewModels;
using Thing.Services;
using Thing.Filters;
namespace Thing.Controllers
{
    public class CatalogController : Controller
    {
        private CatalogService _catalogService;
        private ImageService _imageService;
        IWebHostEnvironment _appEnvironment;
        public CatalogController(CatalogService catalogService, ImageService imageService)
        {
            _catalogService = catalogService;
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CategoriesAsync()
        {
            return View(await _catalogService.GetAllCategoriesAsync());
        }
        public async Task<IActionResult> CategoryProductsAsync(int Id)
        {
            ViewBag.Category = await _catalogService.GetCategoryByIdAsync(Id);
            return View(await _catalogService.GetProductsCardsAsync(Id));
        }
        public async Task<IActionResult> DetailsOfProductAsync(int ProductId, int CategoryId)
        {
            
            ViewBag.Product = await _catalogService.GetProductByIdAsync(ProductId);
            ViewBag.CustomProperties = await _catalogService.GetCustomPropertiesByProductIdAsync(ProductId);
            ViewBag.PropertyValues = await _catalogService.GetPropertyValuesOfProductByProductIdAndCategoryIdAsync(ProductId, CategoryId);
            ViewBag.Category = await _catalogService.GetCategoryByIdAsync(CategoryId);
            return View(await _catalogService.GetProductImagesById(ProductId));
        }
        public async Task<IActionResult> Comments(int ProductId,int  CategoryId)
        {
            ViewBag.Product = await _catalogService.GetProductByIdAsync(ProductId);
            ViewBag.Answers = await _catalogService.GetAnswersCardsByProductIdAsync(ProductId);
            ViewBag.Images = await _imageService.GetAllCommentImagesAsync();
            ViewBag.CategoryId = CategoryId;
            var comments = await _catalogService.GetCommentsCardsByProductIdAsync(ProductId);
            return View(comments);
        }
        [Authorize]
        //[NotBannedFilter]
        public async Task<IActionResult> WriteCommentAsync(Comment comment, int ProductId, int CategoryId, IFormFileCollection uploads)
        {

            if (string.IsNullOrWhiteSpace(_appEnvironment.WebRootPath))
            {
                _appEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            foreach (var image in uploads)
            {
                if (image.Name != null && image.Name!="")
                {
                    string path = "/Images/" + image.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    CommentImage commentImage = new CommentImage() { ImagePath = path, CommentId = comment.Id };
                    await _imageService.AddCommentImageAsync(commentImage);
                }
                
            }
            comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _catalogService.AddCommentAsync(comment);
            return RedirectToAction("Comments", new { ProductId, CategoryId });
        }

        [Authorize]
        public async Task<IActionResult> ToFavorits(int ProductId, int CategoryId)
        {
            var favorit = new Favorite()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ProductId = ProductId
            };
            if (await _catalogService.IsFavoriteExistsAsync(favorit)) return RedirectToAction("DetailsOfProduct", new { ProductId, CategoryId }); 

            await _catalogService.AddFavoriteAsync(favorit);
            return RedirectToAction("CategoryProducts", new { Id = CategoryId });
        }

        [Authorize]
        //[NotBannedFilter]
        public IActionResult WriteAnswer(int CommentId , int ProductId, int CategoryId)
        {
            ViewBag.CommentId = CommentId;
            ViewBag.ProductId = ProductId;
            ViewBag.CategoryId = CategoryId;
            return View();
        }
        [Authorize]
        public async Task<IActionResult> PostAnswerAsync(Answer Answer , int ProductId, int CategoryId)
        {
            Answer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _catalogService.AddAnswerAsync(Answer);
            return RedirectToAction("Comments", new { ProductId, CategoryId });
        }

        [Authorize]
        public async Task<IActionResult> ToCart(int ProductId, int CategoryId)
        {
            var order = new Order() {
                Count=1,
             ProductId = ProductId,
              State = "INCART",
               UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            if (await _catalogService.IsOrderExistsAsync(order)) return RedirectToAction("DetailsOfProduct", new { ProductId, CategoryId });

            await _catalogService.AddOrderAsync(order);

            return RedirectToAction("CategoryProducts", new { Id = CategoryId });
        }
    }
}
