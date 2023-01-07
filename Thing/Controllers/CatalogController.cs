using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Thing.Models;
using Thing.Services;

namespace Thing.Controllers
{
    public class CatalogController : Controller
    {
        private CatalogService _catalogService;
         public CatalogController(CatalogService catalogService)
        {
            _catalogService = catalogService;
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
            ViewBag.Images = await _catalogService.GetAllImagesAsync();
            return View(await _catalogService.FindProductsByCategoryIdAsync(Id));
        }
        public async Task<IActionResult> DetailsOfProductAsync(int Id)
        {
            ViewBag.Product = await _catalogService.GetProductByIdAsync(Id);
            return View(await _catalogService.GetProductImagesById(Id));
        }
        public async Task<IActionResult> CommentsOfProductAsync(int Id)
        {
            ViewBag.Product = await _catalogService.GetProductByIdAsync(Id);
            ViewBag.Answers = await _catalogService.GetAllAnswersAsync();
            ViewBag.Images = await _catalogService.GetAllCommentsImagesAsync();
            return View(await _catalogService.GetProductCommentsByIdAsync(Id));
        }
        public async Task<IActionResult> WriteCommentAsync(Comment comment)
        {
            comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _catalogService.AddCommentAsync(comment);
            return View("Categories", await _catalogService.GetAllCategoriesAsync());
        }

        public async Task<IActionResult> ToFavorits(int Id)
        {
            var favorit = new Favorite()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ProductId = Id
            };
            await _catalogService.AddFavoriteAsync(favorit);
            return View("Categories", await _catalogService.GetAllCategoriesAsync());
        }
        

        public IActionResult WriteAnswer(int CommentId)
        {
           
            return View();
        }
        public IActionResult PostAnswer(int CommentId)
        {

            return View();
        }
    }
}
