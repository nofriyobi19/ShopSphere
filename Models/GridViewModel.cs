namespace ShopSphere.Models;

public class GridViewModel<T> {
    public required List<T> Content { get; set; }
    public required PaginationViewModel Pagination { get; set; }
}