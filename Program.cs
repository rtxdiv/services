using services.Entity;
using services.Services;
using services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using services.ActionFilters;

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

builder.Services.AddScoped<IRootService, RootService>();
builder.Services.AddScoped<IRequestsService, RequestsService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEditorService, EditorService>();

builder.Services.AddScoped<RequireAdminFilter>();
builder.Services.AddScoped<CheckAdminFilter>();

builder.Services.AddScoped<IAuthService>(provider => {
    AppDbContext context = provider.GetRequiredService<AppDbContext>();
    return new AuthService(context, adminKey);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRouting();
app.MapControllers();

app.Use(async (context, next) => {
    if (context.Request.Path.StartsWithSegments("/img/services") && context.Request.Path.Value != null) {

        string filePath = Path.Combine("wwwroot", context.Request.Path.Value.TrimStart('/'));
        if (!File.Exists(filePath)) {
            await context.Response.SendFileAsync(Path.Combine("wwwroot", "img", "noimg.png"));
        } else {
            await next();
        }

    } else {
        await next();
    }
});

app.UseStaticFiles();

app.Run();
