using System.Globalization;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories;

public class OrderRepository(ShopSphereContext shopSphereContext) : CrudRepository<Order, long>(shopSphereContext), IOrderRepository {
    private readonly ShopSphereContext dbContext = shopSphereContext;

    public async Task<GridViewModel<Order>> FindAllAsync(string buyerName, DateTime startDate, DateTime endDate, PaginationViewModel pagination) {
        var orders = dbContext.Orders.Include(e => e.User).Where(e => string.Concat(e.User.FirstName, " ", e.User.LastName).Trim().Contains(buyerName) && e.OrderDate >= startDate && e.OrderDate <= endDate).OrderBy($"{pagination.SortBy} {pagination.Sort}");
        pagination.TotalItems = await orders.CountAsync();
        var orderPaging = await orders.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        return new GridViewModel<Order>(orderPaging, pagination);
    }

    public async Task<GridViewModel<Order>> FindAllByUsernameAsync(string username, DateTime startDate, DateTime endDate, PaginationViewModel pagination) {
        var orders = dbContext.Orders.Include(e => e.User).Where(e => e.User.Username == username && e.OrderDate >= startDate && e.OrderDate <= endDate);
        pagination.TotalItems = await orders.CountAsync();
        var orderPaging = await orders.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        return new GridViewModel<Order>(orderPaging, pagination);
    }
}