using Microsoft.AspNetCore.Mvc.Rendering;
using ShopSphere.Data.Repositories;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models;
using ShopSphere.Models.Products;

namespace ShopSphere.Services;

public class HomeService(ICategoryRepository categoryRepository, IProductRepository productRepository, ICartRepository cartRepository) {
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    private readonly IProductRepository _productRepository = productRepository;

    private readonly ICartRepository _cartRepository = cartRepository;

    public async Task<List<SelectListItem>> GetCategoryDropdownAsync() {
        var categories = await _categoryRepository.FindAllAsync();
        return [.. categories.OrderBy(e => e.Name).Select(e => e.ToSelectListItem())];
    }

    public async Task<ProductGridViewModel> GetAllProductAsync(int pageNumber, int pageSize, string sortBy, string sort, string productName, decimal minPrice, decimal maxPrice, long categoryId) {
        sortBy = sortBy is null || sortBy == "" || sortBy.Equals("entry", StringComparison.CurrentCultureIgnoreCase) ? "ProductId" : sortBy;
        PaginationViewModel pagination = new(pageNumber, pageSize, sortBy, sort);
        var products = await _productRepository.FindAllAsync(productName, minPrice, maxPrice, categoryId, pagination);
        List<SelectListItem> categoryDropdown;
        if (productName != "") {
            var categoriesByProductName = await _categoryRepository.FindAllByProductNameAsync(productName);
            categoryDropdown = [.. categoriesByProductName.OrderBy(e => e.Name).Select(e => e.ToSelectListItem())];
        } else categoryDropdown = await GetCategoryDropdownAsync();
        return new ProductGridViewModel {
            Payload = products.ToProductViewModel(),
            Name = productName,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Category = categoryId,
            CategoryDropdown = categoryDropdown
        };
    }

    public async Task<int> CountCartItemByUsername(string username) {
        return await _cartRepository.CountByUsername(username);
    }
}