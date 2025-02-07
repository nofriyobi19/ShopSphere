using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models.Home;
using ShopSphere.Services;

namespace ShopSphere.Controllers;

[Route("")]
public class HomeController(HomeService service) : Controller {
    private readonly HomeService _service = service;

    [Route("")]
    public async Task<IActionResult> Index() {
        var categoryDropdown = await _service.GetCategoryDropdownAsync();
        var model = new UserNavViewModel {
            CategoryDropdown = categoryDropdown
        };
        return View(model);
    }

    [Route("search")]
    public async Task<IActionResult> Search(int pageNumber, string sortBy, string sort, int pageSize = 16, string productName = "", decimal minPrice = 0, decimal maxPrice = 9999999999, long category = 0) {
        var products = await _service.GetAllProductAsync(pageNumber, pageSize, sortBy, sort, productName, minPrice, maxPrice, category);
        return View(products);
    }
}