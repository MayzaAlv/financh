using financh_backend.Data.Requests;
using financh_backend.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace financh_backend.Controllers
{
    [Route("api/v1/financh/usuario")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult LogarUsuario(LoginRequest request)
        {
            Result resultado = _loginService.LogarUsuario(request);
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Errors.ToList()[0].Message);
            }
            return Ok(resultado.Successes.ToList()[0].Message);
        }

        [HttpPost("solicitar-reset")]
        public IActionResult SolicitarResetSenha(SolicitarSenhaResetRequest request)
        {
            Result resultado = _loginService.SolicitarResetSenha(request);
            if(resultado.IsFailed)
            {
                return Unauthorized();
            }
            return Ok(resultado.Successes.ToList()[0].Message);
        }

        [HttpPost("reset-senha")]
        public IActionResult ResetSenha(ResetSenhaRequest request)
        {
            Result resultado = _loginService.ResetSenha(request);
            if(resultado.IsFailed)
            {
                return Unauthorized();
            }
            return Ok();
        }
    }
}
