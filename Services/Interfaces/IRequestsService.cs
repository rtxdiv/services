using services.Entity;
using services.Models.DtoModels;

namespace services.Services.Interfaces
{
    public interface IRequestsService
    {
        Task<List<Request>> GetRequests(string? userId, bool isAdmin);
        Task<Request?> AcceptRequest(int requestId);
        Task<Request?> RejectRequest(int requestId);
    }
}