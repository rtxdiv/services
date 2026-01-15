using services.Entity;

namespace services.Services.Interfaces
{
    public interface IEditorService
    {
        Task<Service?> GetService(int id);
    }
}