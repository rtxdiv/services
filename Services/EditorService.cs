using Microsoft.EntityFrameworkCore;
using services.Entity;
using services.Services.Interfaces;

namespace services.Services
{
    public class EditorService(AppDbContext db) : IEditorService
    {
        public async Task<Service?> GetService(int id)
        {
            Service? service = await db.Services.Where(e => e.Id == id).FirstOrDefaultAsync();
            return service;
        }
    }
}