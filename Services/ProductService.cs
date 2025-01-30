using Microsoft.AspNetCore.Mvc.Rendering;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models.Products;

namespace ShopSphere.Services;

public class ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository) {
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<List<ProductViewModel>> GetAllProductAsync() {
        var products = await _productRepository.FindAllAsync();
        return [.. products.Select(e => e.ToProductViewModel())];
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
}