using financh_backend.Data.Dtos;
using financh_backend.Data.Requests;
using financh_backend.Models;
using financh_backend.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace financh_backend.Controllers
{
    [Route("api/v1/financh/cadastros")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost("usuario")]
        public IActionResult CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = _cadastroService.CadastrarUsuario(createDto);
            if (resultado.IsFailed) 
            {
                return StatusCode(500);
            }
            return Ok(resultado.Successes.ToList()[0].Message);
        }

        [HttpGet("ativar")]
        public IActionResult AtivarConta([FromQuery] AtivarContaRequest request)
        {
            Result resultado = _cadastroService.AtivarConta(request);
            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
