using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models.Cart;
using ShopSphere.Models.Products;
using ShopSphere.Services;

namespace ShopSphere.Controllers;

[Route("product")]
public class ProductController(ProductService service) : Controller {
    private readonly ProductService _service = service;

    [Route("")]
    public async Task<IActionResult> Index(int pageNumber, int pageSize, string sortBy, string sort, string name = "", decimal minPrice = 0, decimal maxPrice = 9999999999, long category = 0) {
        var products = await _service.GetAllProductAsync(pageNumber, pageSize, sortBy, sort, name, minPrice, maxPrice, category);
        return View(products);
    }

    [Route("upsert/{id?}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Upsert(long id) {
        ProductUpsertViewModel model = id == 0 ? new() : await _service.GetProductUpsertAsync(id);
        model.CategoryDropdown = await _service.GetCategoryDropdownAsync();
        return View(model);
    }

    [Route("save")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Save(ProductUpsertViewModel productUpsertViewModel) {
        await _service.SaveProductAsync(productUpsertViewModel);
        return RedirectToAction("index");
    }

    [Route("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(long id) {
        await _service.DeleteProductById(id);
        return RedirectToAction("index");
    }

    [Route("detail/{id}")]
    public async Task<IActionResult> Detail(long id) {
        var product = await _service.GetProductDetailAsync(id);
        product.CartUpsert = new CartUpsertViewModel(id);
        return View(product);
    }
}