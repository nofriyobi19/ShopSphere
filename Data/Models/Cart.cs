namespace ShopSphere.Data.Models;

public class Cart
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
}