using Microsoft.AspNetCore.Mvc;
using Thing.Models;
using Thing.Services;

namespace Thing.Controllers
{
    // add only admins filter
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index() => View(await _categoryService.List());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            await _categoryService.Create(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] Category category)
        {
            var res = await _categoryService.Edit(category.Id, category.Name);
            return res ? RedirectToAction(nameof(Index)) : View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _categoryService.Delete(id);
            return res ? RedirectToAction(nameof(Index)) : View("Error");
        }
    }
}