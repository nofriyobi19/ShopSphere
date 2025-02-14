using System.Globalization;
using ShopSphere.Configurations;
using ShopSphere.Data.Models;
using ShopSphere.Models;
using ShopSphere.Models.Orders;

namespace ShopSphere.Helpers;

public static class OrderMapper {
    public static OrderViewModel ToOrderViewModel(this Order order) {
        return new OrderViewModel {
            Id = order.OrderId,
            Buyer = $"{order.User.FirstName} {order.User.LastName}".Trim(),
            TotalPrice = string.Format(CultureInfo.GetCultureInfo(CultureConfig.CurrencyCulture), "{0:C}", order.TotalPrice),
            Status = order.Status,
            OrderDate = order.OrderDate
        };
    }

    public static GridViewModel<OrderViewModel> ToOrderViewModel(this GridViewModel<Order> orderGrid) {
        var orders = orderGrid.Content.Select(e => e.ToOrderViewModel()).ToList();
        return new GridViewModel<OrderViewModel>(orders, orderGrid.Pagination);
    }

    public static OrderDetailViewModel ToOrderDetailViewModel(this Order order) {
        return new OrderDetailViewModel {
            Buyer = $"{order.User.FirstName} {order.User.LastName}".Trim(),
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalPrice = order.TotalPrice,
            OrderItems = [.. order.OrderItems.Select(e => e.ToOrderItemViewModel())]
        };
    }
}