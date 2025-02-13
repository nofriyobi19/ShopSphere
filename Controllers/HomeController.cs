using System.Security.Claims;
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

        if (User.Identity!.IsAuthenticated) {
            var username = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;
            model.TotalCartItem = await _service.CountCartItemByUsername(username);
        }

        return View(model);
    }

    [Route("search")]
    public async Task<IActionResult> Search(int pageNumber, string sortBy, string sort, int pageSize = 16, string productName = "", decimal minPrice = 0, decimal maxPrice = 9999999999, long category = 0) {
        var products = await _service.GetAllProductAsync(pageNumber, pageSize, sortBy, sort, productName, minPrice, maxPrice, category);

        var userNavigation = new UserNavViewModel {
            CategoryDropdown = products.CategoryDropdown,
            Category = products.Category,
            ProductName = products.Name
        };

        if (User.Identity!.IsAuthenticated) {
            var username = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;
            userNavigation.TotalCartItem = await _service.CountCartItemByUsername(username);
        }

        var model = new SearchViewModel {
            Products = products,
            Navigation = userNavigation
        };

        return View(model);
    }
}