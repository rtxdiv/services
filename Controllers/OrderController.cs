using Microsoft.AspNetCore.Mvc;
using services.Models.ViewModels;
using services.Entity;
using services.Services.Interfaces;
using services.Models.DtoModels;

namespace services.Controllers
{
    [Route("order")]
    public class OrderController(
        IOrderService orderService,
        IAuthService authService

    ) : Controller
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Order(int id)
        {
            Service service = await orderService.GetService(id);
            if (service == null) return NotFound();

            OrderModel model = new()
            {
                Service = service
            };

            return View(model);
        }

        [HttpPost("/send")]
        public async Task<IActionResult> Send([FromBody] OrderSendDto body)
        {
            string? userId;
            if (Request.Cookies.TryGetValue("user_id", out userId)) {
                if (!await authService.ValidateUserId(userId)) {
                    userId = Guid.NewGuid().ToString();
                }
            } else {
                userId = Guid.NewGuid().ToString();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await orderService.SaveOrder(userId, body);

            Response.Cookies.Append("user_id", userId, new CookieOptions {
                Expires = DateTime.Now.AddDays(100),
                HttpOnly = true
            });
            return Redirect("/requests");
        }
    }
}