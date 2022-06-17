using financh_backend.Data.Requests;
using financh_backend.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace financh_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogarUsuario(LoginRequest request)
        {
            Result resultado = _loginService.LogarUsuario(request);
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Errors.ToList()[0].Message);
            }
            return Ok(resultado.Successes.ToList()[0].Message);
        }
    }
}
