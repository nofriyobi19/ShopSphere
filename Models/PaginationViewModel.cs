using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopSphere.Models;

public class PaginationViewModel {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SortBy { get; set; } = null!;
    public string Sort { get; set; } = null!;
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    public int TotalItems { get; set; }
    public List<SelectListItem> PageSizeDropdown => [
        new() { Text = "10", Value = "10" },
        new() { Text = "30", Value = "30" },
        new() { Text = "50", Value = "50" }
    ];
    public List<SelectListItem> SortDropdown => [
        new() { Text = "Asc", Value = "asc" },
        new() { Text = "Desc", Value = "desc" }
    ];
}