using AutoMapper;
using FluentResults;
using Operacoes.Data;
using Operacoes.Data.Dtos.Gasto;
using Operacoes.Models;

namespace Operacoes.Services
{
    public class GastoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GastoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GastoDto AdicionarGasto(GastoDto gastoDto, int usuarioId)
        {
            Gasto gasto = _mapper.Map<Gasto>(gastoDto);
            gasto.UsuarioId = usuarioId;
            _context.Gastos.Add(gasto);
            _context.SaveChanges();

            return gastoDto;
        }

        public List<GastoDto> MostrarGastoAtual(int usuarioId)
        {
            List<Gasto> gastos = _context.Gastos.Where(gastoUsuario => gastoUsuario.UsuarioId == usuarioId)
                                    .Where(g => g.DataGasto.Month == DateTime.Now.Month).ToList();
            if (gastos != null)
            {
                return _mapper.Map<List<GastoDto>>(gastos);
            }
            return null;
        }


        public List<GastoDto> MostrarGastoMes(int usuarioId, int data)
        {
            List<Gasto> gastos = _context.Gastos.Where(gastoUsuario => gastoUsuario.UsuarioId == usuarioId)
                                 .Where(g => g.DataGasto.Month == data).ToList();
            if (gastos != null)
            {
                return _mapper.Map<List<GastoDto>>(gastos);
            }
            return null;
        }

        public Result AtualizarGasto(int id, int usuarioId, GastoDto gastoDto)
        {
            Gasto gasto = _context.Gastos.Where(gastoUsuario => gastoUsuario.UsuarioId == usuarioId)
                                         .Where(g => g.Id == id).FirstOrDefault();
            if(gasto == null)
            {
                return Result.Fail("Falha na procura");
            }
            _mapper.Map(gastoDto, gasto);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletarGasto(int id, int usuarioId)
        {
            Gasto gasto = _context.Gastos.Where(gastoUsuario => gastoUsuario.UsuarioId == usuarioId)
                                         .Where(g => g.Id == id).FirstOrDefault();
            if(gasto == null)
            {
                return Result.Fail("Falha na procura");
            }
            _context.Remove(gasto);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
