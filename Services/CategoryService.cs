using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models;
using ShopSphere.Models.Categories;

namespace ShopSphere.Services;

public class CategoryService(ICategoryRepository categoryRepository) {
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryGridViewModel> GetAllCategoryAsync(int pageNumber, int pageSize, string sortBy, string sort, string name) {
        PaginationViewModel pagination = new() {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber,
            PageSize = pageSize <= 0 ? 10 : pageSize,
            SortBy = sortBy is null || sortBy.Equals("entry", StringComparison.CurrentCultureIgnoreCase) ? "CategoryId" : sortBy,
            Sort = sort is null || sort == "asc" ? "asc" : "desc"
        };
        var categoryGrid = await _categoryRepository.FindAllAsync(name, pagination);
        return new CategoryGridViewModel {
            Payload = categoryGrid.ToCategoryViewModel(),
            Name = name
        };
    }

    public async Task<CategoryViewModel> GetCategoryById(long categoryId) {
        var category = await _categoryRepository.FindByIdAsync(categoryId);
        return category.ToCategoryViewModel();
    }

    public async Task<CategoryViewModel> SaveCategoryAsync(CategoryViewModel categoryViewModel) {
        var category = await _categoryRepository.SaveAsync(categoryViewModel.ToCategory());
        return category.ToCategoryViewModel();
    }

    public async Task<CategoryViewModel> DeleteCategoryByIdAsync(long categoryId) {
        var category = await _categoryRepository.FindByIdAsync(categoryId);
        await _categoryRepository.DeleteAsync(category);
        return category.ToCategoryViewModel();
    }

    public async Task<int> GetCategoryCountAsync() {
        var categories = await _categoryRepository.FindAllAsync();
        return categories.Count;
    }
}