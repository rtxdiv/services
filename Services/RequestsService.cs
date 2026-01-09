using services.Entity;
using services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace services.Services
{
    public class RequestsService(AppDbContext db) : IRequestsService
    {
        public async Task<List<Request>> GetRequests(string? userId, bool isAdmin)
        {
            if (isAdmin) {
                return await db.Requests.ToListAsync();

            } else {
                return await db.Requests.Where(e => e.UserId == userId).ToListAsync();
            }
        }
    }
}