namespace ShopSphere.Models;

public class GridViewModel<T>(List<T> content, PaginationViewModel pagination) {
    public List<T> Content { get; set; } = content;
    
    public PaginationViewModel Pagination { get; set; } = pagination;
}