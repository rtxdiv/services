using services.Entity;
using services.Models.DtoModels;

namespace services.Services.Interfaces
{
    public interface IEditorService
    {
        Task<Service?> GetService(int id);
        Task CreateService(EditorCreateDto body);
        Task<Service?> UpdateService(EditorUpdateDto body);
    }
}