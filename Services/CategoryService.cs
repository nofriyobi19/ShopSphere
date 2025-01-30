using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models.Categories;

namespace ShopSphere.Services;

public class CategoryService(ICategoryRepository categoryRepository) {
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<List<CategoryViewModel>> GetAllCategoryAsync() {
        var categories = await _categoryRepository.FindAllAsync();
        return [.. categories.Select(e => e.ToCategoryViewModel())];
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
}