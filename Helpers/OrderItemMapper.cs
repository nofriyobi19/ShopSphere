using ShopSphere.Data.Models;
using ShopSphere.Models.OrderItem;

namespace ShopSphere.Helpers;

public static class OrderItemMapper {
    public static OrderItemViewModel ToOrderItemViewModel(this OrderItem orderItem) {
        return new OrderItemViewModel {
            ProductName = orderItem.Product.Name,
            Price = orderItem.Product.Price,
            Quantity = orderItem.Quantity
        };
    }
}