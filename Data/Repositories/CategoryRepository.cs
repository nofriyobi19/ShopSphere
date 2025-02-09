using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories;

public class CategoryRepository(ShopSphereContext shopSphereContext) : CrudRepository<Category, long>(shopSphereContext), ICategoryRepository {
    private readonly ShopSphereContext dbContext = shopSphereContext;

    public async Task<GridViewModel<Category>> FindAllAsync(string name, PaginationViewModel pagination) {
        var categories = dbContext.Categories.Where(e => e.Name.Contains(name)).OrderBy($"{pagination.SortBy} {pagination.Sort}");
        pagination.TotalItems = await categories.CountAsync();
        var categoryPaging = await categories.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        return new GridViewModel<Category>(categoryPaging, pagination);
    }

    public async Task<List<Category>> FindAllByProductNameAsync(string productName) {
        return await dbContext.Categories.Include(e => e.Products).Where(e => e.Products.Any(e => e.Name.Contains(productName))).ToListAsync();
    }
}