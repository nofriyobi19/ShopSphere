using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories;

public class ProductRepository(ShopSphereContext shopSphereContext) : CrudRepository<Product, long>(shopSphereContext), IProductRepository {
    private readonly ShopSphereContext dbContext = shopSphereContext;

    public override async Task<List<Product>> FindAllAsync() {
        return await dbContext.Products.Include(e => e.Category).ToListAsync();
    }

    public override async Task<Product> FindByIdAsync(long id) {
        return await dbContext.Products.Include(e => e.Category).SingleAsync(e => e.ProductId == id);
    }

    public async Task<GridViewModel<Product>> FindAllAsync(string name, decimal minPrice, decimal maxPrice, long categoryId, PaginationViewModel pagination) {
        var products = dbContext.Products.Include(e => e.Category).Where(e => e.Name.Contains(name) && e.Price >= minPrice && e.Price <= maxPrice && (categoryId == 0 || e.CategoryId == categoryId)).OrderBy($"{pagination.SortBy} {pagination.Sort}");
        pagination.TotalItems = await products.CountAsync();
        var productPaging = await products.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        return new GridViewModel<Product>(productPaging, pagination);
    }
}