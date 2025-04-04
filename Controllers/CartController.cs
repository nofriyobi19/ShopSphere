using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models.Cart;
using ShopSphere.Models.Home;
using ShopSphere.Services;

namespace ShopSphere.Controllers;

[Route("cart")]
[Authorize(Roles = "User")]
public class CartController(CartService service) : Controller {
    private readonly CartService _service = service;

    [Route("")]
    public async Task<IActionResult> Index(int pageNumber, int pageSize, string sortBy, string sort) {
        var username = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;

        var cartList = await _service.GetAllUserCartItem(pageNumber, pageSize, sortBy, sort, username);

        var userNavigation = new UserNavViewModel {
            CategoryDropdown = await _service.GetCategoryDropdown(),
            TotalCartItem = await _service.CountCartItemByUsername(username)
        };

        var model = new CartGridViewModel {
            Payload = cartList,
            Navigation = userNavigation
        };

        return View(model);
    }

    [Route("upsert/{id?}")]
    public async Task<IActionResult> Upsert(long id, long productId) {
        CartUpsertViewModel model = id == 0 ? new CartUpsertViewModel { ProductId = productId } : await _service.GetCartUpsertAsync(id);
        return View(model);
    }

    [Route("save")]
    public async Task<IActionResult> Save(CartUpsertViewModel cartUpsertViewModel) {
        var username = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;
        await _service.SaveCartAsync(username, cartUpsertViewModel);
        if (cartUpsertViewModel.Id != 0) return RedirectToAction("index");
        return RedirectToAction("search", "home");
    }

    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(long id) {
        await _service.DeleteCartByIdAsync(id);
        return RedirectToAction("index");
    }

    [Route("checkout")]
    public async Task<IActionResult> Checkout() {
        var username = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;
        await _service.CheckoutAllCartAsync(username);
        return RedirectToAction("user", "order");
    }
}