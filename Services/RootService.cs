using services.Entity;
using services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace services.Services
{
    public class RootService(AppDbContext db, IAuthService authService) : IRootService
    {
        public async Task<List<Service>> GetServices(bool isAdmin)
        {
            if (isAdmin) {
                return await db.Services.ToListAsync();
            }
            return await db.Services.Where(e => e.Visible).ToListAsync();
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

            if (service.Image != null) await RemoveImage(service.Image);

            return service;
        }

        public async Task<int> CountNoti(bool isAdmin, HttpContext context)
        {
            if (isAdmin) {
                return await db.Requests.Where(e => e.AdminNoti).CountAsync();

            } else {
                Validation validation = await authService.ValidateUser(context);
                if (validation.Valide) return validation.NotiCount;
            }
            return 0;
        }

        private static async Task RemoveImage(string fileName)
        {
            string filePath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot", "img", "services", fileName);
            await Task.Run(() => File.Delete(filePath));
        }
    }
}