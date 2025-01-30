using ShopSphere.Configurations;

var builder = WebApplication.CreateBuilder(args);

DependencyInjection.AddServices(builder.Services);
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();