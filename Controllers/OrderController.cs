using System.Data.SqlTypes;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Services;

namespace ShopSphere.Controllers;

[Route("order")]
public class OrderController(OrderService service) : Controller {
    private readonly OrderService _service = service;

    [Route("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index(int pageNumber, int pageSize, string sortBy, string sort, DateTime? startDate, DateTime? endDate, string buyerName = "") {
        startDate ??= SqlDateTime.MinValue.Value;
        endDate ??= SqlDateTime.MaxValue.Value;
        var orders = await _service.GetAllOrderAsync(pageNumber, pageSize, sortBy, sort, buyerName, startDate.Value, endDate.Value);
        if (orders.StartDate == SqlDateTime.MinValue.Value) orders.StartDate = null;
        if (orders.EndDate == SqlDateTime.MaxValue.Value) orders.EndDate = null;
        return View(orders);
    }

    [Route("user")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Index(int pageNumber, int pageSize, string sortBy, string sort, DateTime? startDate, DateTime? endDate) {
        startDate ??= SqlDateTime.MinValue.Value;
        endDate ??= SqlDateTime.MaxValue.Value;
        var username = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;
        var orders = await _service.GetAllUserOrderAsync(pageNumber, pageSize, sortBy, sort, username, startDate.Value, endDate.Value);
        if (orders.StartDate == SqlDateTime.MinValue.Value) orders.StartDate = null;
        if (orders.EndDate == SqlDateTime.MaxValue.Value) orders.EndDate = null;
        return View("UserIndex", orders);
    }

    [Route("detail/{id}")]
    [Authorize]
    public async Task<IActionResult> Detail(long id) {
        var order = await _service.GetOrderDetail(id);
        if (User.IsInRole("User")) {
            var username = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;
            order.UserNavigation = await _service.GetUserNavigation(username);
        }
        return View(order);
    }
}