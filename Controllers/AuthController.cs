using Microsoft.AspNetCore.Mvc;
using services.Models.DtoModels;
using services.Models.ViewModels;
using services.Services.Interfaces;

namespace services.Controllers
{
    [Route("auth")]
    public class AuthController(IAuthService authService) : Controller
    {
        [HttpGet]
        public IActionResult Auth()
        {
            return View(new BaseLayoutModel());
        }

        [HttpPost("/trykey")]
        public IActionResult TryKey([FromBody] AuthTrykeyDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (authService.VerifyKey(body.Key)) {
                Response.Cookies.Append("admin_key", authService.Hash(body.Key), new CookieOptions {
                    Expires = DateTime.Now.AddDays(100), HttpOnly = true
                });
                return Redirect("/");

            } else {
                return NotFound("Неправильный ключ");
            }
        }
    }
}