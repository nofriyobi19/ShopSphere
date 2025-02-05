using Microsoft.EntityFrameworkCore;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories;

public class UserRepository(ShopSphereContext shopSphereContext) : CrudRepository<User, long>(shopSphereContext), IUserRepository {
    private readonly ShopSphereContext dbContext = shopSphereContext;

    public async Task<GridViewModel<User>> FindAllAsync(string username, string name, string email, string role, PaginationViewModel pagination) {
        var users = dbContext.Users.Where(e => e.Username.Contains(username) && e.Email.Contains(email) && e.Role.Contains(role));
        pagination.TotalItems = users.Count();
        var userPaging = await users.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        return new GridViewModel<User>(userPaging, pagination);
    }

    public async Task<User> FindByUsernameAsync(string username) {
        return await dbContext.Users.SingleAsync(e => e.Username == username);
    }
}