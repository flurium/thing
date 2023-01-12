using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dal.Models;
using Dal.Services;
using Dal.Filters;
using Domain.Models;

namespace Dal.Controllers
{
    [Authorize(Roles = Roles.Seller)]
    [NotBannedFilter]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ProductImageService _productImageService;
        private readonly RequiredPropertiesService _requiredPropertiesService;
        private readonly RequiredPropertyValueService _requiredPropertyValue;
        public readonly CustomPropertyService _customPropertyService;
        private readonly IWebHostEnvironment _host;

        public ProductController(ProductService productService, CategoryService categoryService, ProductImageService productImageService, IWebHostEnvironment webHost, RequiredPropertiesService requiredPropertiesService, RequiredPropertyValueService requiredPropertyValue, CustomPropertyService customPropertyService)
        {
            _productService = productService;
            _productImageService = productImageService;
            _categoryService = categoryService;
            _host = webHost;
            _requiredPropertiesService = requiredPropertiesService;
            _requiredPropertyValue = requiredPropertyValue;
            _customPropertyService = customPropertyService;
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
            Product product = new()
            {
                Name = productView.Name,
                Price = productView.Price,
                SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Description = productView.Description,
                Status = productView.Status,
                CategoryId = productView.CategoryId
            };
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

            return RedirectToAction("RequiredProperty", "Product", new { CategoryId = res.CategoryId, ProductId = res.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productImageService.DeleteFromServer(id);
            await _productService.Delete(id);
            return RedirectToAction("Profile", "Seller");
        }

        public async Task<IActionResult> RequiredProperty(int CategoryId, int ProductId)
        {
            var property = await _requiredPropertiesService.FindByConditioAsync(x => x.CategoryId == CategoryId);
            ViewBag.ProductId = ProductId;
            ViewBag.Prorerty = property;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RequiredProperty(ListPropertyValueViewModel propList, int ProductId, bool CheckProp)
        {
            for (var i = 0; i < propList.Values.Count; i++)
            {
                RequiredPropertyValue required = new RequiredPropertyValue { ProductId = ProductId, PropertyId = propList.PropertyId[i], Value = propList.Values[i] };
                await _requiredPropertyValue.CreateAsync(required);
            }

            if (!CheckProp)
            {
                return RedirectToAction("Profile", "Seller");
            }
            else
            {
                return RedirectToAction("CustomProperties", "Product", new { ProductId = ProductId });
            }
        }

        public IActionResult CustomProperties(int ProductId)
        {
            ViewBag.ProductId = ProductId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomProperties(CustomProperty property, bool CheckProp, int ProductId)
        {
            CustomProperty customProperty = new CustomProperty { Name = property.Name, Value = property.Value, ProductId = ProductId };
            await _customPropertyService.CreateAsync(customProperty);
            if (CheckProp)
            {
                return RedirectToAction("CustomProperties", "Product", new { ProductId = ProductId });
            }

            return RedirectToAction("Profile", "Seller");
        }
    }
}