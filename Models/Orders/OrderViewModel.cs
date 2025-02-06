namespace ShopSphere.Models.Orders;

public class OrderViewModel {
    public long Id { get; set; }

    public string Buyer { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;

    public DateTime OrderDate { get; set; } 
}