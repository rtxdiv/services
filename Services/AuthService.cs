using Microsoft.EntityFrameworkCore;
using services.Entity;
using services.Services.Interfaces;

namespace services.Services
{
    class AuthService(AppDbContext db) : IAuthService
    {
        public async Task<bool> ValidateUserId(string userId)
        {
            List<Request> requests = await db.Requests.ToListAsync();
            if (requests.Count == 0) return false;
            return true;
        }
    }
}