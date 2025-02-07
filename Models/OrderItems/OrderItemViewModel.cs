namespace ShopSphere.Models.OrderItem;

public class OrderItemViewModel {
    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}