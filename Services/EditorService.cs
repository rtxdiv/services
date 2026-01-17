using Microsoft.EntityFrameworkCore;
using services.Entity;
using services.Models.DtoModels;
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

        public async Task CreateService(EditorCreateDto body)
        {
            string? image = null;
            if (body.Image != null) image = await WriteImage(body.Image);

            db.Services.Add(new Service
            {
                Name = body.Name,
                Description = body.Description,
                Requirements = body.Requirements,
                Image = image
            });

            await db.SaveChangesAsync();
        }

        public async Task<Service?> UpdateService(EditorUpdateDto body)
        {
            Service? service = await db.Services.Where(e => e.Id == body.GetServiceId()).FirstOrDefaultAsync();
            if (service == null) return null;

            string? image = service.Image;

            if (body.Image != null) {
                if (image != null) {
                    await RemoveImage(image);
                }
                image = await WriteImage(body.Image);
            }

            service.Name = body.Name;
            service.Description = body.Description;
            service.Requirements = body.Requirements;
            service.Image = image;

            await db.SaveChangesAsync();

            return service;
        }

        private static async Task<string> WriteImage(IFormFile file)
        {
            Random random = new();

            string fileName = DateTime.Now.ToString("dd.MM.yy-HH.mm.ss-") + random.Next(1000).ToString("D3") + Path.GetExtension(file.FileName);

            string folder = Path.Join(Directory.GetCurrentDirectory(), "wwwroot", "img", "services");
            Directory.CreateDirectory(folder);

            string filePath = Path.Join(folder, fileName);

            using Stream stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }

        private static async Task RemoveImage(string fileName)
        {
            string filePath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot", "img", "services", fileName);
            await Task.Run(() => File.Delete(filePath));
        }
    }
}