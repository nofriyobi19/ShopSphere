using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopSphere.Models.Products;

public class ProductGridViewModel {
    public required GridViewModel<ProductViewModel> Payload { get; set; }

    public string Name { get; set; } = null!;

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public long Category { get; set; }

    public required List<SelectListItem> CategoryDropdown { get; set; }
}