using ShopSphere.Data.Models;
using ShopSphere.Models;
using ShopSphere.Models.Orders;

namespace ShopSphere.Helpers;

public static class OrderMapper {
    public static OrderViewModel ToOrderViewModel(this Order order) {
        return new OrderViewModel {
            Id = order.OrderId,
            Buyer = $"{order.User.FirstName} {order.User.LastName}".Trim(),
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            OrderDate = order.OrderDate
        };
    }

    public static GridViewModel<OrderViewModel> ToOrderViewModel(this GridViewModel<Order> orderGrid) {
        var orders = orderGrid.Content.Select(e => e.ToOrderViewModel()).ToList();
        return new GridViewModel<OrderViewModel>(orders, orderGrid.Pagination);
    }
}