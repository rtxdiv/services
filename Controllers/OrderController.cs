using Microsoft.AspNetCore.Mvc;
using services.Models.ViewModels;
using services.Entity;
using services.Services.Interfaces;

namespace services.Controllers
{
    [Route("order")]
    public class OrderController(IOrderService orderService) : Controller
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
    }
}