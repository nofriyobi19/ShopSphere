namespace ShopSphere.Models.Cart;

public class CartViewModel {
    public long Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }
}