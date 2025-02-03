namespace ShopSphere.Models.Categories;

public class CategoryGridViewModel {
    public required GridViewModel<CategoryViewModel> Payload { get; set; }
    public string Name { get; set; } = null!;
}