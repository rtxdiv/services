using Microsoft.AspNetCore.Mvc;
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
                Admin = false,
                Layout = {
                    ShowRequests = true,
                }
            };

            return View(model);
        }
    }
}
