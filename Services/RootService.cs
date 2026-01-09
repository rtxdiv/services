using services.Entity;
using services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace services.Services
{
    public class RootService(AppDbContext db) : IRootService
    {
        public async Task<List<Service>> GetServices()
        {
            return await db.Services.ToListAsync();
        }
    }
}