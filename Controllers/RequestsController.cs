using Microsoft.AspNetCore.Mvc;
using services.ActionFilters;
using services.Entity;
using services.Models.DtoModels;
using services.Models.ViewModels;
using services.Services.Interfaces;

namespace services.Controllers
{
    [Route("requests")]
    public class RequestsController(
        IRequestsService requestsService,
        IAuthService authService
    ) : Controller
    {
        [HttpGet]
        [HttpGet("{status}")]
        [ServiceFilter(typeof(CheckAdminFilter))]
        public async Task<IActionResult> Requests(string status)
        {
            if (!(status == "accepted" || status == "rejected" || status == "waiting" || status == "all")) {
                return Redirect("/requests/all");
            }

            bool isAdmin = HttpContext.Items["admin"] as bool? ?? false;
            List<Request> requests = [];

            Validation validation = await authService.ValidateUser(HttpContext);
            if (validation.Valide == true) {
                requests = await requestsService.GetRequests(validation.UserId, isAdmin);
            }

            RequestsModel model = new()
            {
                Requests = requests,
                Admin = isAdmin,
                Status = status,
                RequestsCount = validation.RequestsCount
            };

            return View(model);
        }

        [HttpPost("/accept")]
        [ServiceFilter(typeof(RequireAdminFilter))]
        public async Task<IActionResult> AcceptRequest([FromBody] AcceptRequestDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Request? request = await requestsService.AcceptRequest(body.RequestId);
            if (request == null) return NotFound("Запрос не найден");
            return Ok(new {
                status = request.Status,
                text = request.StatusText
            });
        }

        [HttpPost("/reject")]
        [ServiceFilter(typeof(RequireAdminFilter))]
        public async Task<IActionResult> RejectRequest([FromBody] AcceptRequestDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Request? request = await requestsService.RejectRequest(body.RequestId);
            if (request == null) return NotFound("Запрос не найден");
            return Ok(new {
                status = request.Status,
                text = request.StatusText
            });
        }
    }
}