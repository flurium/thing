using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // delete product
        // want add ban counter to seller to ban seller if counter ~= 10
        //public async Task<IActionResult> Product()
        //{
        //}
    }
}