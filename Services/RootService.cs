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

        public async Task<Service?> ChangeVisibility(int id)
        {
            Service? service = await db.Services.Where(e => e.Id == id).FirstAsync();
            if (service == null) return null;

            service.Visible = !service.Visible;
            await db.SaveChangesAsync();
            return service;
        }

        public async Task<Service?> DeleteService(int id)
        {
            Service? service = await db.Services.Where(e => e.Id == id).FirstAsync();
            if (service == null) return null;

            db.Services.Remove(service);
            await db.SaveChangesAsync();
            return service;
        }
    }
}