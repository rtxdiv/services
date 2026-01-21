using Microsoft.AspNetCore.Mvc;
using services.ActionFilters;
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
        [ServiceFilter(typeof(CheckAdminFilter))]
        public async Task<IActionResult> Home()
        {
            bool isAdmin = HttpContext.Items["admin"] as bool? ?? false;

            HomeModel model = new() {
                Services = await rootService.GetServices(isAdmin),
                Admin = isAdmin,
                Layout = {
                    Admin = isAdmin,
                    ShowRequests = true,
                    ShowNotification = await rootService.CountNoti(isAdmin, HttpContext) > 0
                }
            };

            return View(model);
        }

        [HttpPost("/changeVisibility")]
        [ServiceFilter(typeof(RequireAdminFilter))]
        public async Task<IActionResult> ChangeVisibility([FromBody] ChangeVisibilityDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Service? service = await rootService.ChangeVisibility(body.ServiceId);
            if (service == null) return NotFound("Услуга не найдена");

            return Ok(new { visible = service.Visible });
        }

        [HttpPost("/deleteService")]
        [ServiceFilter(typeof(RequireAdminFilter))]
        public async Task<IActionResult> DeleteService([FromBody] DeleteServiceDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Service? service = await rootService.DeleteService(body.ServiceId);
            if (service == null) return NotFound("Услуга не найдена");

            return Ok();
        }
    }
}
