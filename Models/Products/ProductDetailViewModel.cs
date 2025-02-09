using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopSphere.Models.Products;

public class ProductDetailViewModel {
    public long Id { get; set; }

    public string Category { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public required List<SelectListItem> CategoryDropdown { get; set; }
}