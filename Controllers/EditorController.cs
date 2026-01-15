using Microsoft.AspNetCore.Mvc;
using services.Entity;
using services.Models.ViewModels;
using services.Services.Interfaces;

namespace services.Controllers
{
    [Route("editor")]
    public class EditorController(IEditorService editorService) : Controller
    {
        [HttpGet]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Editor(int id = -1)
        {
            EditorModel model = new();

            if (id == -1) {
                model.New = true;

            } else {
                Service? service = await editorService.GetService(id);
                if (service == null) return NotFound("Услуга не найдена");

                model.ServiceId = service.Id;
                model.Name = service.Name;
                model.Description = service.Description;
                model.Requirements = service.Requirements;
                model.Image = service.Image ?? model.Image;
            }

            return View(model);
        }
    }
}