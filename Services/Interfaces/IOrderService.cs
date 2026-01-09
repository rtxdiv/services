using services.Entity;

namespace services.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Service> GetService(int id);
        Task SaveOrder(Request request);
    }
}
