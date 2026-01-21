using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using services.Entity;
using services.Services.Interfaces;

namespace services.Services
{
    class AuthService(AppDbContext db, string adminKey) : IAuthService
    {
        public bool VerifyHash(string hashedKey)
        {
            try {
                return BCrypt.Net.BCrypt.Verify(adminKey, hashedKey);
            } catch {
                return false;
            }
        }

        public string Hash(string row)
        {
            return BCrypt.Net.BCrypt.HashPassword(row);
        }

        public bool VerifyKey(string key)
        {
            return key == adminKey;
        }

        public async Task<Validation> ValidateUser(HttpContext context, [Optional] VParams vparams)
        {
            bool valide = true;
            string? userId;
            List<Request> requests;

            try {

                userId = context.Request.Cookies["user_id"] ?? throw new NotValidException();
                requests = await db.Requests.Where(e => e.UserId == userId).ToListAsync();
                if (requests.Count == 0) throw new NotValidException();

                context.Response.Cookies.Append("user_id", userId, new CookieOptions {
                    Expires = DateTime.Now.AddDays(100),
                    HttpOnly = true
                });
            }
            catch (NotValidException) {

                valide = false;
                requests = [];

                if (vparams?.NewId == true) {
                    userId = Guid.NewGuid().ToString();
                    context.Response.Cookies.Append("user_id", userId, new CookieOptions {
                        Expires = DateTime.Now.AddDays(100),
                        HttpOnly = true
                    });

                } else {
                    userId = null;
                    context.Response.Cookies.Delete("user_id");
                }
            }

            return new Validation {
                Valide = valide,
                UserId = userId,
                RequestsCount = requests.Count,
                NotiCount = requests.Count(e => e.UserNoti)
            };
        }
    }
}