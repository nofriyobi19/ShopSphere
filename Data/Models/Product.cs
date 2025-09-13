namespace ShopSphere.Data.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public long CategoryId { get; set; }
}