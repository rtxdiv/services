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
            Service? service = await orderService.GetService(id);
            if (service == null) return NotFound("Услуга не найдена");

            OrderModel model = new()
            {
                Service = service
            };

            return View(model);
        }

        [HttpPost("/send")]
        public async Task<IActionResult> Send([FromBody] OrderSendDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Validation validation = await authService.ValidateUser(HttpContext, new VParams { NewId = true });

            Request? request = await orderService.SaveOrder(validation.UserId ?? throw new Exception(), body);
            if (request == null) return NotFound("Услуга не найдена");

            return Redirect("/requests");
        }
    }
}