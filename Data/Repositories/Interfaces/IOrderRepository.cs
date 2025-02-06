using ShopSphere.Data.Models;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories.Interfaces;

public interface IOrderRepository : ICrudRepository<Order, long> {
    Task<GridViewModel<Order>> FindAllAsync(string buyerName, DateTime startDate, DateTime endDate, PaginationViewModel pagination);

    Task<GridViewModel<Order>> FindAllByUsernameAsync(string username, DateTime startDate, DateTime endDate, PaginationViewModel pagination);
}