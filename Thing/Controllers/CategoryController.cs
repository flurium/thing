using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thing.Models;
using Thing.Services;

namespace Thing.Controllers
{
    // add only admins filter
    [Authorize(Roles = Roles.Admin)]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly RequiredPropertiesService _requiredPropertiesService;

        public CategoryController(CategoryService categoryService, RequiredPropertiesService requiredPropertiesService)
        {
            _categoryService = categoryService;
            _requiredPropertiesService = requiredPropertiesService;
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

        public async Task<IActionResult> Properties(int id)
        {
            var category = await _categoryService.PropertiesFor(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProperty(int categoryId, string propertyName)
        {
            await _requiredPropertiesService.Create(categoryId, propertyName);
            return RedirectToAction(nameof(Properties), new { id = categoryId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProperty(int Id, string Name, int categoryId)
        {
            var res = await _requiredPropertiesService.Edit(Id, Name);
            return res ? RedirectToAction(nameof(Properties), new { id = categoryId }) : View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProperty(int categoryId, int propertyId)
        {
            var res = await _requiredPropertiesService.Delete(propertyId);
            return res ? RedirectToAction(nameof(Properties), new { id = categoryId }) : View("Error");
        }
    }
}