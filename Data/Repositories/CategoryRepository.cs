using Microsoft.EntityFrameworkCore;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;

namespace ShopSphere.Data.Repositories;

public class CategoryRepository(ShopSphereContext shopSphereContext) : CrudRepository<Category, long>(shopSphereContext), ICategoryRepository {

}