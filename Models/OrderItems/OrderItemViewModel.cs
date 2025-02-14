namespace ShopSphere.Models.OrderItem;

public class OrderItemViewModel {
    public string ProductName { get; set; } = null!;

    public string Price { get; set; } = null!;

    public int Quantity { get; set; }
}