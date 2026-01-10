using services.Entity;
using services.Models.DtoModels;

namespace services.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Service> GetService(int id);
        Task SaveOrder(string userId, OrderSendDto request);
    }
}
