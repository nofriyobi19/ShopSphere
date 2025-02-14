namespace ShopSphere.Models.Orders;

public class OrderViewModel {
    public long Id { get; set; }

    public string Buyer { get; set; } = null!;

    public string TotalPrice { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime OrderDate { get; set; } 
}