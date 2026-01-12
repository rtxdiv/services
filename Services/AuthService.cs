using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using services.Entity;
using services.Services.Interfaces;

namespace services.Services
{
    class AuthService(AppDbContext db) : IAuthService
    {
        public async Task<Validation> ValidateUser(HttpContext context, [Optional] VParams vparams)
        {
            bool valide = true;
            string? userId;
            int requestsCount = 0;

            try {

                userId = context.Request.Cookies["user_id"] ?? throw new NotValidException();
                requestsCount = await db.Requests.Where(e => e.UserId == userId).CountAsync();
                if (requestsCount == 0) throw new NotValidException();

                context.Response.Cookies.Append("user_id", userId, new CookieOptions { Expires = DateTime.Now.AddDays(100), HttpOnly = true });
            }
            catch (NotValidException) {

                valide = false;

                if (vparams?.NewId == true) {
                    userId = Guid.NewGuid().ToString();
                    context.Response.Cookies.Append("user_id", userId, new CookieOptions { Expires = DateTime.Now.AddDays(100), HttpOnly = true });

                } else {
                    userId = null;
                    context.Response.Cookies.Delete("user_id");
                }
            }

            return new Validation {
                Valide = valide,
                UserId = userId,
                RequestsCount = requestsCount
            };
        }
    }
}