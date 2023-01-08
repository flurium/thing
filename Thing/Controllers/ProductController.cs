using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Thing.Models;
using Thing.Models.ViewModels;
using Thing.Services;

namespace Thing.Controllers
{
    [Authorize(Roles = Roles.Seller)]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ProductImageService _productImageService;
        private IWebHostEnvironment _host;

        public ProductController(ProductService productService, CategoryService categoryService, ProductImageService productImageService, IWebHostEnvironment webHost)
        {
            _productService = productService;
            _productImageService = productImageService;
            _categoryService = categoryService;
            _host = webHost;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.List();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productView)
        {
            Product product = new Product();
            product.Name = productView.Name;
            product.Price = productView.Price;
            product.SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            product.Description = productView.Description;
            product.Status = productView.Status;
            product.CategoryId = productView.CategoryId;
            var res = await _productService.CreateAsync(product);

            string filePath = Path.Combine(_host.WebRootPath, "Images");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            foreach (var uploadedFile in productView.Url)
            {
                // путь к папке Files
                string path = "/Images/" + uploadedFile.FileName;

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_host.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                ProductImage img = new() { ProductId = res.Id, Url = path };

                await _productImageService.CreateAsync(img);
            }

            return RedirectToAction("Profile", "Seller");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("Profile", "Seller");
        }
    }
}