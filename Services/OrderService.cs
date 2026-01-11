using services.Entity;
using services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using services.Models.DtoModels;

namespace services.Services
{
    public class OrderService(AppDbContext db) : IOrderService
    {
        public async Task<Service> GetService(int id)
        {
            return await db.Services.Where(e => e.Id == id).FirstAsync();
        }

        public async Task SaveOrder(string userId, OrderSendDto dto)
        {
            Service service = await db.Services.Where(e => e.Id == dto.ServiceId).FirstAsync() ?? throw new Exception("Услуга не найдена");
            Request request = new() {
                UserId = userId,
                ServiceName = service.Name,
                Query = dto.Query,
                Contact = dto.Contact,
                StatusText = "В ожидании"
            };
            db.Requests.Add(request);
            await db.SaveChangesAsync();
        }
    }
}