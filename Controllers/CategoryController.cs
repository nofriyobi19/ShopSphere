using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models.Categories;
using ShopSphere.Services;

namespace ShopSphere.Controllers;

[Route("category")]
[Authorize(Roles = "Admin")]
public class CategoryController(CategoryService service) : Controller {
    private readonly CategoryService _service = service;

    [Route("")]
    public async Task<IActionResult> Index(int pageNumber, int pageSize, string sortBy, string sort, string name = "") {
        var categories = await _service.GetAllCategoryAsync(pageNumber, pageSize, sortBy, sort, name);
        return View(categories);
    }

    [Route("upsert/{id?}")]
    public async Task<IActionResult> Upsert(long id) {
        CategoryViewModel model = id == 0 ? new() : await _service.GetCategoryById(id);
        return View(model);
    }

    [Route("save")]
    public async Task<IActionResult> Save(CategoryViewModel categoryViewModel) {
        await _service.SaveCategoryAsync(categoryViewModel);
        return RedirectToAction("index");
    }

    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(long id) {
        await _service.DeleteCategoryByIdAsync(id);
        return RedirectToAction("index");
    }
}