using Microsoft.AspNetCore.Mvc;
using services.Entity;
using services.Models.DtoModels;
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
            bool isAdmin = true;
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

        [HttpPost("/accept")]
        public async Task<IActionResult> AcceptRequest([FromBody] AcceptRequestDto body)
        {
            Request? request = await requestsService.AcceptRequest(body.RequestId);
            if (request == null) return NotFound();
            return Ok(new {
                status = request.Status,
                text = request.StatusText
            });
        }

        [HttpPost("/reject")]
        public async Task<IActionResult> RejectRequest([FromBody] AcceptRequestDto body)
        {
            Request? request = await requestsService.RejectRequest(body.RequestId);
            if (request == null) return NotFound();
            return Ok(new {
                status = request.Status,
                text = request.StatusText
            });
        }
    }
}