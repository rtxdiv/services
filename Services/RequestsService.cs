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
                return await db.Requests.OrderByDescending(e => e.CreatedAt).ToListAsync();
            } else {
                return await db.Requests.Where(e => e.UserId == userId).ToListAsync();
            }
        }

        public async Task<Request?> AcceptRequest(int requestId)
        {
            Request? request = await db.Requests.FindAsync(requestId);
            if (request == null) return null;

            request.Status = true;
            request.StatusText = "Одобрено";
            await db.SaveChangesAsync();
            return request;
        }

        public async Task<Request?> RejectRequest(int requestId)
        {
            Request? request = await db.Requests.FindAsync(requestId);
            if (request == null) return null;

            request.Status = false;
            request.StatusText = "Отклонено";
            await db.SaveChangesAsync();
            return request;
        }
    }
}