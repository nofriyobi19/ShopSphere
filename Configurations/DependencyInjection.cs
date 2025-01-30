using ShopSphere.Data;
using ShopSphere.Data.Repositories;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Services;

namespace ShopSphere.Configurations;

public static class DependencyInjection {
    public static void AddServices(IServiceCollection services) {
        services.AddDbContext<ShopSphereContext>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<CategoryService>();
    }
}