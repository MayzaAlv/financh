using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operacoes.Data.Dtos.Gasto;
using Operacoes.Models;
using Operacoes.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Operacoes.Controllers
{
    [ApiController]
    [Route("api/v1/financh/gasto")]
    public class GastoController : ControllerBase
    {
        GastoService _gastoService;

        public GastoController(GastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [HttpPost("gasto")]
        [Authorize]
        public IActionResult AdicionarGasto([FromBody] GastoDto gastoDto)
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = ResgatarIdToken(token);
            GastoDto readGasto = _gastoService.AdicionarGasto(gastoDto, usuarioId);

            return Ok(readGasto);
        }

        [HttpGet("gasto")]
        [Authorize]
        public IActionResult MostrarGastoAtual()
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = ResgatarIdToken(token);

            List<GastoDto> gastoDto = _gastoService.MostrarGastoAtual(usuarioId);
            if (gastoDto == null)
            {
                throw new Exception("Falha na procura");
            }
            return Ok(gastoDto);
        }

        [HttpGet("gastos")]
        [Authorize]
        public IActionResult MostrarGasto(string mes)
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = ResgatarIdToken(token);
            
            List<GastoDto> gastoDto = _gastoService.MostrarGastoMes(usuarioId, int.Parse(mes));
            if(gastoDto == null)
            {
                throw new Exception("Falha na procura");
            }
            return Ok(gastoDto);
        }

        [HttpPut("gasto")]
        [Authorize]
        public IActionResult AtualizarGasto(int id, [FromBody] GastoDto gastoDto)
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = ResgatarIdToken(token);

            Result resultado = _gastoService.AtualizarGasto(id, usuarioId, gastoDto);
            if(resultado.IsFailed)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("gasto")]
        [Authorize]
        public IActionResult DeletarGasto(int id)
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            int usuarioId = ResgatarIdToken(token);

            Result resultado = _gastoService.DeletarGasto(id, usuarioId);
            if (resultado.IsFailed)
            {
                return NotFound();
            }
            return NoContent();
        }

        public static int ResgatarIdToken(string token)
        {
            token = token.Substring(7);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            return int.Parse(tokenS.Claims.FirstOrDefault(claim => claim.Type == "id").Value);
        }
    }
}
