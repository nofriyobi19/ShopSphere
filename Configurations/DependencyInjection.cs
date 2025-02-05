using ShopSphere.Data;
using ShopSphere.Data.Repositories;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Services;

namespace ShopSphere.Configurations;

public static class DependencyInjection {
    public static void AddServices(IServiceCollection services, IConfigurationManager configuration) {
        services.AddSqlServer<ShopSphereContext>(configuration.GetConnectionString("DefaultConnection"));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<CategoryService>();
        services.AddScoped<ProductService>();
        services.AddScoped<AccountService>();
    }
}