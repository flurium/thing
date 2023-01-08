using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Thing.Models;
using Thing.Services;

namespace Thing.Controllers
{
    [Authorize(Roles = Roles.Seller)]
    public class SellerController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly ProductService _productService;
        private readonly ProductImageService _productImageService;

        public SellerController(SellerService sellerService, ProductService productService, ProductImageService productImage)
        {
            _sellerService = sellerService;
            _productService = productService;
            _productImageService = productImage;
        }


        public async Task<IActionResult> Profile()
        {
            ViewBag.Images = await _productImageService.List();
            return View(await _productService.FindByConditionAsync(x => x.SellerId == User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
