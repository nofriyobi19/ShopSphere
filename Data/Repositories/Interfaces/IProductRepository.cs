using ShopSphere.Data.Models;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories.Interfaces;

public interface IProductRepository : ICrudRepository<Product, long> {
    Task<GridViewModel<Product>> FindAllAsync(string name, decimal minPrice, decimal maxPrice, long categoryId, PaginationViewModel pagination);
}