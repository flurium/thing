using Microsoft.AspNetCore.Mvc;
using Thing.Models;
using Thing.Services;

namespace Thing.Controllers
{
    public class SellerController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly ProductService _productService;
        public SellerController(SellerService sellerService, ProductService productService)
        {
            _sellerService = sellerService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(new List<Product> { new Product { Name = "temp", Description = "sssssssssssssssssssssssssssssssssssss", Price = 3500, Status = "New" }, new Product { Name = "temp", Description = "sssssssssssssssssssssssssssssssssssss", Price = 3500, Status = "New" } });
            
           // return View(_productService.FindByConditionAsync(x=>x.Seller==User.Identity));
        }
    }
}
