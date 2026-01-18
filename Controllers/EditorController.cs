using Microsoft.AspNetCore.Mvc;
using services.ActionFilters;
using services.Entity;
using services.Models.DtoModels;
using services.Models.ViewModels;
using services.Services.Interfaces;

namespace services.Controllers
{
    [Route("editor")]
    public class EditorController(IEditorService editorService) : Controller
    {
        [HttpGet]
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(RequireAdminFilter))]
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

        [HttpPost("/create")]
        public async Task<IActionResult> Create([FromForm] EditorCreateDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await editorService.CreateService(body);

            return Redirect("/");
        }

        [HttpPost("/update")]
        public async Task<IActionResult> Update([FromForm] EditorUpdateDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Service? service = await editorService.UpdateService(body);
            if (service == null) return NotFound();

            return Redirect("/");
        }
    }
}