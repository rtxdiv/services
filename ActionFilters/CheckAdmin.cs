using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using services.Services.Interfaces;

namespace services.ActionFilters
{
    public class CheckAdminFilter(IAuthService authService) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? key = context.HttpContext.Request.Cookies["admin_key"];

            if (key == null || !authService.VerifyHash(key))
            {
                context.HttpContext.Response.Cookies.Delete("admin_key");

            } else {
                context.HttpContext.Items["admin"] = true;
                context.HttpContext.Response.Cookies.Append("admin_key", key, new CookieOptions{
                    Expires = DateTime.Now.AddDays(100),
                    HttpOnly = true
                });
            }

            await next();
        }
    }
}