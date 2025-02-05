using Microsoft.AspNetCore.Authentication.Cookies;
using ShopSphere.Configurations;

var builder = WebApplication.CreateBuilder(args);

DependencyInjection.AddServices(builder.Services, builder.Configuration);
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();