using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using services.Services.Interfaces;

namespace services.ActionFilters
{
    public class RequireAdminFilter(IAuthService authService) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? key = context.HttpContext.Request.Cookies["admin_key"];

            if (key == null || !authService.VerifyHash(key))
            {
                context.HttpContext.Response.Cookies.Delete("admin_key");
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            context.HttpContext.Response.Cookies.Append("admin_key", key, new CookieOptions{
                Expires = DateTime.Now.AddDays(100),
                HttpOnly = true
            });

            await next();
        }
    }
}