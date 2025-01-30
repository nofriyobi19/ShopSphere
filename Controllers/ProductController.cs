using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models.Products;
using ShopSphere.Services;

namespace ShopSphere.Controllers;

[Route("product")]
public class ProductController(ProductService service) : Controller {
    private readonly ProductService _service = service;

    [Route("")]
    public async Task<IActionResult> Index() {
        var products = await _service.GetAllProductAsync();
        return View(products);
    }

    [Route("upsert/{id?}")]
    public async Task<IActionResult> Upsert(long id) {
        ProductUpsertViewModel model = id == 0 ? new() : await _service.GetProductUpsertAsync(id);
        model.CategoryDropdown = await _service.GetCategoryDropdownAsync();
        return View(model);
    }

    [Route("save")]
    public async Task<IActionResult> Save(ProductUpsertViewModel productUpsertViewModel) {
        await _service.SaveProductAsync(productUpsertViewModel);
        return RedirectToAction("index");
    }

    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(long id) {
        await _service.DeleteProductById(id);
        return RedirectToAction("index");
    }
}