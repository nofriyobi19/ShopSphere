using Microsoft.AspNetCore.Mvc.Rendering;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models;
using ShopSphere.Models.Products;

namespace ShopSphere.Services;

public class ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, ICartRepository cartRepository) {
    private readonly IProductRepository _productRepository = productRepository;

    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    private readonly ICartRepository _cartRepository = cartRepository;

    public async Task<ProductGridViewModel> GetAllProductAsync(int pageNumber, int pageSize, string sortBy, string sort, string name, decimal minPrice, decimal maxPrice, long categoryId) {
        sortBy = sortBy is null || sortBy == "" || sortBy.Equals("entry", StringComparison.CurrentCultureIgnoreCase) ? "ProductId" : sortBy;
        PaginationViewModel pagination = new(pageNumber, pageSize, sortBy, sort);
        var products = await _productRepository.FindAllAsync(name, minPrice, maxPrice, categoryId, pagination);
        var categoryDropdown = await GetCategoryDropdownAsync();
        return new ProductGridViewModel {
            Payload = products.ToProductViewModel(),
            Name = name,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Category = categoryId,
            CategoryDropdown = categoryDropdown
        };
    }

    public async Task<ProductUpsertViewModel> GetProductUpsertAsync(long productId) {
        var product = await _productRepository.FindByIdAsync(productId);
        return product.ToProductUpsertViewModel();
    }

    public async Task<List<SelectListItem>> GetCategoryDropdownAsync() {
        var categories = await _categoryRepository.FindAllAsync();
        return [.. categories.OrderBy(e => e.Name).Select(e => e.ToSelectListItem())];
    }

    public async Task<ProductViewModel> SaveProductAsync(ProductUpsertViewModel productUpsertViewModel) {
        var savedProduct = await _productRepository.SaveAsync(productUpsertViewModel.ToProduct());
        var product = await _productRepository.FindByIdAsync(savedProduct.ProductId);
        return product.ToProductViewModel();
    }

    public async Task<ProductViewModel> DeleteProductById(long productId) {
        var product = await _productRepository.FindByIdAsync(productId);
        await _productRepository.DeleteAsync(product);
        return product.ToProductViewModel();
    }

    public async Task<ProductDetailViewModel> GetProductDetailAsync(long productId) {
        var product = await _productRepository.FindByIdAsync(productId);
        var categoryDropdown = await GetCategoryDropdownAsync();
        return product.ToProductDetailViewModel(categoryDropdown);
    }

    public async Task<int> CountCartItemByUsername(string username) {
        return await _cartRepository.CountByUsername(username);
    }
}