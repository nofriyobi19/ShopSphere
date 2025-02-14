using ShopSphere.Models.Home;
using ShopSphere.Models.OrderItem;

namespace ShopSphere.Models.Orders;

public class OrderDetailViewModel {
    public string Buyer { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public string TotalPrice { get; set; } = null!;

    public required List<OrderItemViewModel> OrderItems;

    public UserNavViewModel? UserNavigation { get; set; }
}