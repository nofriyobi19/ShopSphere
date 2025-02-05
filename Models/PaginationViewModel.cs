using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopSphere.Models;

public class PaginationViewModel(int pageNumber, int pageSize, string sortBy, string sort) {
    public int PageNumber { get; set; } = pageNumber == 0 ? 1 : pageNumber;

    public int PageSize { get; set; } = pageSize == 0 ? 10 : pageSize;

    public string SortBy { get; set; } = sortBy;

    public string Sort { get; set; } = sort == "desc" ? "desc" : "asc";

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