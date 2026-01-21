using services.Entity;
using services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace services.Services
{
    public class RequestsService(AppDbContext db) : IRequestsService
    {
        public async Task<List<Request>> GetRequests(string? userId, bool isAdmin)
        {
            List<Request> requests;
            if (isAdmin) {
                requests = await db.Requests.OrderByDescending(e => e.CreatedAt).ToListAsync();
            } else {
                requests = await db.Requests.Where(e => e.UserId == userId).ToListAsync();
            }

            List<Request>? requestsCopy = JsonSerializer.Deserialize<List<Request>>(
                JsonSerializer.Serialize(requests)
            );

            if (isAdmin) {
                foreach (Request request in requests) {
                    if (request.AdminNoti) request.AdminNoti = false;
                }

            } else {
                foreach (Request request in requests) {
                    if (request.UserNoti) request.UserNoti = false;
                }
            }
            await db.SaveChangesAsync();

            return requestsCopy ?? [];
        }

        public async Task<Request?> AcceptRequest(int requestId)
        {
            Request? request = await db.Requests.FindAsync(requestId);
            if (request == null) return null;

            request.Status = true;
            request.StatusText = "Одобрено";
            request.UserNoti = true;
            await db.SaveChangesAsync();
            return request;
        }

        public async Task<Request?> RejectRequest(int requestId)
        {
            Request? request = await db.Requests.FindAsync(requestId);
            if (request == null) return null;

            request.Status = false;
            request.StatusText = "Отклонено";
            request.UserNoti = true;
            await db.SaveChangesAsync();
            return request;
        }
    }
}