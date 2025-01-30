namespace ShopSphere.Models.Products;

public class ProductViewModel {
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string CategoryName { get; set; } = null!;
}