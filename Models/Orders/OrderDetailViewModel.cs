using ShopSphere.Models.OrderItem;

namespace ShopSphere.Models.Orders;

public class OrderDetailViewModel {
    public string Buyer { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public required List<OrderItemViewModel> OrderItems;
}