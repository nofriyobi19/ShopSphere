using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopSphere.Models.Home;

public class UserNavViewModel {
    public required List<SelectListItem> CategoryDropdown { get; set; }

    public string ProductName { get; set; } = null!;

    public long Category { get; set; }
}