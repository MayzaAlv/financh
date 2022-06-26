using financh_backend.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace financh_backend.Controllers
{
    [Route("api/v1/financh/usuario")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost("logout")]
        public IActionResult DeslogarUsuario()
        {
            Result resultado = _logoutService.DeslogarUsuario();
            if(resultado.IsFailed)
            {
                return Unauthorized();
            }
            return Ok();
        }
    }
}
