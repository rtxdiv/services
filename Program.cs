var builder = WebApplication.CreateBuilder(args);

// сервисы
builder.Services.AddSingleton<IMainService, MainService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.Use(async (context, next) => {
    await next();
    // подолждать все middleware

    // если результат 404, меняем Req и делаем middleware заново
    if (context.Response.StatusCode == 404) {
        context.Response.Redirect("/");
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=MainPage}"
);

app.Run();
