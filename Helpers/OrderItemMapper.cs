using System.Globalization;
using ShopSphere.Configurations;
using ShopSphere.Data.Models;
using ShopSphere.Models.OrderItem;

namespace ShopSphere.Helpers;

public static class OrderItemMapper {
    public static OrderItemViewModel ToOrderItemViewModel(this OrderItem orderItem) {
        return new OrderItemViewModel {
            ProductName = orderItem.Product.Name,
            Price = string.Format(CultureInfo.GetCultureInfo(CultureConfig.CurrencyCulture), "{0:C}", orderItem.Price),
            Quantity = orderItem.Quantity
        };
    }
}