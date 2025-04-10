using Microsoft.AspNetCore.Mvc.Rendering;
using ShopSphere.Data.Models;
using ShopSphere.Models;
using ShopSphere.Models.Categories;

namespace ShopSphere.Helpers;

public static class CategoryMapper {
    public static CategoryViewModel ToCategoryViewModel(this Category category) {
        return new CategoryViewModel {
            Id = category.CategoryId,
            Name = category.Name
        };
    }

    public static Category ToCategory(this CategoryViewModel categoryViewModel) {
        return new Category {
            CategoryId = categoryViewModel.Id,
            Name = categoryViewModel.Name
        };
    }

    public static SelectListItem ToSelectListItem(this Category category) {
        return new SelectListItem {
            Value = category.CategoryId.ToString(),
            Text = category.Name
        };
    }

    public static GridViewModel<CategoryViewModel> ToCategoryViewModel(this GridViewModel<Category> categoryGrid) {
        var categoryViewModels = categoryGrid.Content.Select(e => e.ToCategoryViewModel()).ToList();
        return new GridViewModel<CategoryViewModel>(categoryViewModels, categoryGrid.Pagination);
    }
}