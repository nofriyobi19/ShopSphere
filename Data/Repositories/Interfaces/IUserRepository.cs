using ShopSphere.Data.Models;
using ShopSphere.Models;

namespace ShopSphere.Data.Repositories.Interfaces;

public interface IUserRepository : ICrudRepository<User, long> {
    Task<GridViewModel<User>> FindAllAsync(string username, string name, string email, string role, PaginationViewModel pagination);

    Task<User> FindByUsernameAsync(string username);
}