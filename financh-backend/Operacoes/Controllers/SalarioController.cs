using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operacoes.Data.Dtos.Salario;
using Operacoes.Services;

namespace Operacoes.Controllers
{
    [ApiController]
    [Route("api/v1/financh/salarios")]
    public class SalarioController : ControllerBase
    {
        SalarioService _salarioService;

        public SalarioController(SalarioService salarioService)
        {
            _salarioService = salarioService;
        }

        [HttpPost("salario")]
        [Authorize]
        public IActionResult AdicionarSalario([FromBody] SalarioDto salarioDto)
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = GastoController.ResgatarIdToken(token);

            SalarioIdDto readSalario = _salarioService.AdicionarSalario(salarioDto, usuarioId);

            return CreatedAtAction(nameof(SalarioPorId), new { Id = readSalario.Id }, readSalario);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult SalarioPorId(int id)
        {
            SalarioIdDto readSalario = _salarioService.SalarioPorId(id);

            return Ok(readSalario);
        }

        [HttpGet("salario")]
        [Authorize]
        public IActionResult MostrarSalarioAtual()
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = GastoController.ResgatarIdToken(token);

            SalarioDto salarioDto = _salarioService.MostrarSalarioAtual(usuarioId);
            if(salarioDto == null)
            {
                throw new Exception("Falha na procura");
            }
            return Ok(salarioDto);
        }

        [HttpGet("{mes}/{ano}")]
        [Authorize]
        public IActionResult MostrarSalarioMes(int mes, int ano)
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = GastoController.ResgatarIdToken(token);

            SalarioDto salarioDto = _salarioService.MostrarSalarioMes(usuarioId, mes, ano);
            if(salarioDto == null)
            {
                throw new Exception("Falha na procura");
            }
            return Ok(salarioDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult AtualizarSalario(int id, [FromBody] SalarioDto salarioDto)
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = GastoController.ResgatarIdToken(token);

            Result resultado = _salarioService.AtualizarSalario(id, usuarioId, salarioDto);
            if(resultado.IsFailed)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeletarSalario(int id)
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = GastoController.ResgatarIdToken(token);

            Result resultado = _salarioService.DeletarSalario(usuarioId, id);
            if (resultado.IsFailed)
            {
                return NotFound();
            }
            return Ok(resultado);
        }
    }
}
