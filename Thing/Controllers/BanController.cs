using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Thing.Models;
using Thing.Services;

namespace Thing.Controllers
{
    // To ban different things

    [Authorize(Roles = Roles.Admin)]
    public class BanController : Controller
    {
        private BanService _banService;

        public BanController(BanService banService)
        {
            _banService = banService;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        public async Task<IActionResult> Buyer(string uid, string redirect)
        {
            await _banService.BuyerAsync(uid);
            await _banService.SellerAsync(uid);
            return Redirect(redirect);
        }

        public async Task<IActionResult> Seller(string uid, string redirect)
        {
            await _banService.SellerAsync(uid);
            return Redirect(redirect);
        }

        // delete product
        // want add ban counter to seller to ban seller if counter ~= 10
        public async Task<IActionResult> Product()
        {
        }
        
    }
}