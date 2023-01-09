using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Thing.Models;
using Thing.Services;

namespace Thing.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly OrderService _orderService;

        public CartController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index() => View(await _orderService.FindIncludeProductsAsync(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)));

        
        public async Task<IActionResult> AddToCart(int productId)
        {
            var order = await _orderService.Get(User.FindFirstValue(ClaimTypes.NameIdentifier), productId);

            if (order == null)
            {
                await _orderService.CreateAsync(
                    new Order
                    {
                        ProductId = productId,
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Count = 1,
                        State = "INCART"
                    });
            }
            else
            {
                await _orderService.Increase(order.UserId, order.ProductId);
            }

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            await _orderService.DeleteAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), productId);
            return RedirectToAction("Index");
        }
    }
}