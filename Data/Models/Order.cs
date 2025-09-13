namespace ShopSphere.Data.Models;

public class Order
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}