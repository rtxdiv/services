using services.Entity;

namespace services.Services.Interfaces
{
    public interface IRequestsService
    {
        Task<List<Request>> GetRequests(string? userId, bool isAdmin);
        Task<Request?> AcceptRequest(int requestId);
        Task<Request?> RejectRequest(int requestId);
    }

    public enum RequestStatus
    {
        Accepted, Rejected, Waiting, All
    }
}