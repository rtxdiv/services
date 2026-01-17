using services.Entity;

namespace services.Services.Interfaces
{
    public interface IRootService
    {
        Task<List<Service>> GetServices(bool isAdmin);
        Task<Service?> ChangeVisibility(int id);
        Task<Service?> DeleteService(int id);
    }
}