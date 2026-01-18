using services.Entity;
using services.Services;
using services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// DbContext
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

// configuration
Env.Load(".env.secret");
string? adminKey = Env.GetString("ADMIN_KEY");
if (string.IsNullOrEmpty(adminKey)) {
    throw new InvalidOperationException("Set ADMIN_KEY={value} in .env.secret");
}

// сервисы
builder.Services.AddScoped<IRootService, RootService>();
builder.Services.AddScoped<IRequestsService, RequestsService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEditorService, EditorService>();

builder.Services.AddScoped<IAuthService>(provider => {
    AppDbContext context = provider.GetRequiredService<AppDbContext>();
    return new AuthService(context, adminKey);
});

// контроллеры
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Root}/{action=Home}"
);

app.Run();
