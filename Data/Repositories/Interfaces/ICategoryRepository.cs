using ShopSphere.Data.Models;
using ShopSphere.Models;
using ShopSphere.Models.Categories;

namespace ShopSphere.Data.Repositories.Interfaces;

public interface ICategoryRepository : ICrudRepository<Category, long> {
    Task<GridViewModel<Category>> FindAllAsync(string name, PaginationViewModel pagination);

    Task<List<Category>> FindAllByProductNameAsync(string productName);
}