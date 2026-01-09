using services.Entity;
using services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace services.Services
{
    public class OrderService(AppDbContext db) : IOrderService
    {
        public async Task<Service> GetService(int id)
        {
            return await db.Services.Where(e => e.Id == id).FirstAsync();
        }

        public async Task SaveOrder(Request request)
        {
            throw new Exception();
        }
    }
}