using Microsoft.AspNetCore.Mvc.Rendering;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models;
using ShopSphere.Models.Home;
using ShopSphere.Models.Orders;

namespace ShopSphere.Services;

public class OrderService(IOrderRepository orderRepository, ICategoryRepository categoryRepository, ICartRepository cartRepository) {
    private readonly IOrderRepository _orderRepository = orderRepository;

    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    private readonly ICartRepository _cartRepository = cartRepository;

    public async Task<OrderGridViewModel> GetAllOrderAsync(int pageNumber, int pageSize, string sortBy, string sort, string buyerName, DateTime startDate, DateTime endDate) {
        sortBy = sortBy is null || sortBy == "" || sortBy.Equals("entry", StringComparison.CurrentCultureIgnoreCase) ? "OrderId" : sortBy;
        PaginationViewModel pagination = new(pageNumber, pageSize, sortBy, sort);
        var orders = await _orderRepository.FindAllAsync(buyerName, startDate, endDate, pagination);
        return new OrderGridViewModel {
            Payload = orders.ToOrderViewModel(),
            BuyerName = buyerName,
            StartDate = startDate,
            EndDate = endDate
        };
    }

    public async Task<UserOrderGridViewModel> GetAllUserOrderAsync(int pageNumber, int pageSize, string sortBy, string sort, string username, DateTime startDate, DateTime endDate) {
        sortBy = sortBy is null || sortBy == "" || sortBy.Equals("entry", StringComparison.CurrentCultureIgnoreCase) ? "OrderId" : sortBy;
        PaginationViewModel pagination = new(pageNumber, pageSize, sortBy, sort);
        var orders = await _orderRepository.FindAllByUsernameAsync(username, startDate, endDate, pagination);
        return new UserOrderGridViewModel {
            Payload = orders.ToOrderViewModel(),
            StartDate = startDate,
            EndDate = endDate,
            Navigation = await GetUserNavigation(username)
        };
    }

    public async Task<OrderDetailViewModel> GetOrderDetail(long orderId) {
        var order = await _orderRepository.FindByIdAsync(orderId);
        return order.ToOrderDetailViewModel();
    }

    public async Task<UserNavViewModel> GetUserNavigation(string username) {
        var categories = await _categoryRepository.FindAllAsync();
        var cartItemCount = await _cartRepository.CountByUsername(username);
        return new() {
            CategoryDropdown = [.. categories.Select(e => e.ToSelectListItem())],
            TotalCartItem = cartItemCount
        };
    }


}