using Microsoft.AspNetCore.Mvc;
using services.Entity;
using services.Models.DtoModels;
using services.Models.ViewModels;
using services.Services.Interfaces;

namespace services.Controllers
{
    [Route("/")]
    public class RootController(IRootService rootService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            HomeModel model = new() {
                Services = await rootService.GetServices(),
                Admin = true,
                Layout = {
                    ShowRequests = true,
                }
            };

            return View(model);
        }

        [HttpPost("/changeVisibility")]
        public async Task<IActionResult> ChangeVisibility([FromBody] ChangeVisibilityDto body)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Service? service = await rootService.ChangeVisibility(body.ServiceId);
            if (service == null) return NotFound("Услуга не найдена");

            return Ok(new { visible = service.Visible });
        }

        [HttpPost("/deleteService")]
        public async Task<IActionResult> DeleteService([FromBody] DeleteServiceDto body)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Service? service = await rootService.DeleteService(body.ServiceId);
            if (service == null) return NotFound("Услуга не найдена");

            return Ok();
        }
    }
}
