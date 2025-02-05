using ShopSphere.Data.Models;
using ShopSphere.Models;
using ShopSphere.Models.Products;

namespace ShopSphere.Helpers;

public static class ProductMapper {
    public static ProductViewModel ToProductViewModel(this Product product) {
        return new ProductViewModel {
            Id = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryName = product.Category.Name
        };
    }

    public static ProductUpsertViewModel ToProductUpsertViewModel(this Product product) {
        return new ProductUpsertViewModel {
            Id = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };
    }

    public static Product ToProduct(this ProductUpsertViewModel productUpsertViewModel) {
        return new Product {
            ProductId = productUpsertViewModel.Id,
            Name = productUpsertViewModel.Name,
            Description = productUpsertViewModel.Description,
            Price = productUpsertViewModel.Price,
            Stock = productUpsertViewModel.Stock,
            CategoryId = productUpsertViewModel.CategoryId
        };
    }

    public static GridViewModel<ProductViewModel> ToProductViewModel(this GridViewModel<Product> productGrid) {
        var productViewModels = productGrid.Content.Select(e => e.ToProductViewModel()).ToList();
        return new GridViewModel<ProductViewModel>(productViewModels, productGrid.Pagination);
    }
}