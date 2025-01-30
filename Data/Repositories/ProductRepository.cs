using Microsoft.EntityFrameworkCore;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;

namespace ShopSphere.Data.Repositories;

public class ProductRepository(ShopSphereContext shopSphereContext) : CrudRepository<Product, long>(shopSphereContext), IProductRepository {
    private readonly ShopSphereContext dbContext = shopSphereContext;

    public override async Task<List<Product>> FindAllAsync() {
        return await dbContext.Products.Include(e => e.Category).ToListAsync();
    }

    public override async Task<Product> FindByIdAsync(long id) {
        return await dbContext.Products.Include(e => e.Category).SingleAsync(e => e.ProductId == id);
    }
}