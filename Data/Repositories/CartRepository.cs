using Microsoft.EntityFrameworkCore;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories;

public class CartRepository(ShopSphereContext shopSphereContext) : CrudRepository<Cart, long>(shopSphereContext), ICartRepository {
    private readonly ShopSphereContext dbContext = shopSphereContext;

    public async Task<GridViewModel<Cart>> FindAllAsync(string username, PaginationViewModel pagination) {
        var cartList = dbContext.Carts.Include(e => e.Product).Include(e => e.User).Where(e => e.User.Username == username);
        pagination.TotalItems = cartList.Count();
        var cartListPaging = await cartList.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        return new GridViewModel<Cart>(cartListPaging, pagination);
    }

    public override async Task<Cart> FindByIdAsync(long id) {
        return await dbContext.Carts.Include(e => e.Product).SingleAsync(e => e.CartId == id);
    }

    public async Task<Cart?> FindByProductId(long productId) {
        return await dbContext.Carts.SingleOrDefaultAsync(e => e.ProductId == productId);
    }
}