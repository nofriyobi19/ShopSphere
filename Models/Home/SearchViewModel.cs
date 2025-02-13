using ShopSphere.Models.Products;

namespace ShopSphere.Models.Home;

public class SearchViewModel {
    public required ProductGridViewModel Products { get; set; }

    public required UserNavViewModel Navigation { get; set; }
}