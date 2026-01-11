using Microsoft.AspNetCore.Mvc;
using services.Entity;
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
            bool isAdmin = false;
            List<Request> requests = [];

            if (Request.Cookies.TryGetValue("user_id", out string? userId)) {
                requests = await requestsService.GetRequests(userId, isAdmin);
                if (requests.Count == 0) {
                    Response.Cookies.Delete("user_id");
                } else {
                    Response.Cookies.Append("user_id", userId, new CookieOptions {
                        Expires = DateTime.Now.AddDays(100),
                        HttpOnly = true
                    });
                }
            }

            RequestsModel model = new()
            {
                Requests = requests,
                Admin = isAdmin
            };

            return View(model);
        }
    }
}