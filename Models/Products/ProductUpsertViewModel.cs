using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopSphere.Models.Products;

public class ProductUpsertViewModel {
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    [Display(Name = "Category")]
    public long CategoryId { get; set; }

    public List<SelectListItem>? CategoryDropdown { get; set; }
}