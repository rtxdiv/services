using Microsoft.AspNetCore.Mvc;
using services.Models.ViewModels;
using services.Services.Interfaces;

namespace services.Controllers
{
    [Route("requests")]
    public class RequestsController(IRequestsService requestsService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Requests()
        {
            /* string? userId = HttpContext.Request.Cookies["user_id"]; */
            string? userId = "id1";
            bool isAdmin = true;

            RequestsModel model = new()
            {
                Requests = await requestsService.GetRequests(userId, isAdmin),
                Admin = isAdmin
            };

            return View(model);
        }
    }
}