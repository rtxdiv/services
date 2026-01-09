using services.Entity;

namespace services.Services.Interfaces
{
    public interface IRootService
    {
        Task<List<Service>> GetServices();
    }
}