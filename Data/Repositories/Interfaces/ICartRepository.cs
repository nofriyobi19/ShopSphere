using ShopSphere.Data.Models;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories.Interfaces;

public interface ICartRepository : ICrudRepository<Cart, long> {
    Task<GridViewModel<Cart>> FindAllAsync(string username, PaginationViewModel pagination);

    Task<Cart?> FindByProductId(long productId);
}