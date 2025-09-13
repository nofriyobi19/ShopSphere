using ShopSphere.Data;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Server=LAPTOP-9P7OECVU\\SQLEXPRESS;Database=shop_sphere;User Id=nobidev;Password=Nofriyobi19.;TrustServerCertificate=True";
builder.Services.AddSqlServer<AppDbContext>(connectionString);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
