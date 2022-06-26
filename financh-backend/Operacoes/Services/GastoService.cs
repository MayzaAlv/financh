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

        public GastoIdDto AdicionarGasto(GastoDto gastoDto, int usuarioId)
        {
            Gasto gasto = _mapper.Map<Gasto>(gastoDto);
            List<Gasto> gastoUsuario = _context.Gastos.Where(g => g.UsuarioId == usuarioId).ToList();
            
            foreach (Gasto _gasto in gastoUsuario)
            {
                if(_gasto.DataGasto.Month == gasto.DataGasto.Month 
                    && _gasto.DataGasto.Year == gasto.DataGasto.Year)
                {
                    throw new Exception("Data de gasto existente no banco");
                }
            }

            gasto.UsuarioId = usuarioId;
            _context.Gastos.Add(gasto);
            _context.SaveChanges();

            gasto = _context.Gastos.Where(gasto => gasto.UsuarioId == usuarioId)
                                   .FirstOrDefault(g => g.DataGasto == gasto.DataGasto);

            return _mapper.Map<GastoIdDto>(gasto);
        }

        public GastoIdDto GastoPorId(int gastoId)
        {
            Gasto gasto = _context.Gastos.FirstOrDefault(g => g.Id == gastoId);

            if (gasto != null)
            {
                GastoIdDto gastoDto = _mapper.Map<GastoIdDto>(gasto);

                return gastoDto;
            }
            return null;
        }

        public GastoDto MostrarGastoAtual(int usuarioId)
        {
            Gasto gasto = _context.Gastos.Where(gastoUsuario => gastoUsuario.UsuarioId == usuarioId)
                                        .FirstOrDefault(g => g.DataGasto.Month == DateTime.Now.Month);
            if (gasto != null);
            {
                return _mapper.Map<GastoDto>(gasto);
            }
            return null;
        }


        public GastoDto MostrarGastoMes(int usuarioId, int mes, int ano)
        {
            Gasto gasto = _context.Gastos.Where(gastoUsuario => gastoUsuario.UsuarioId == usuarioId)
                                         .Where(gt => gt.DataGasto.Year == ano)
                                         .FirstOrDefault(g => g.DataGasto.Month == mes);
            if (gasto != null)
            {
                return _mapper.Map<GastoDto>(gasto);
            }
            return null;
        }

        public Result AtualizarGasto(int id, int usuarioId, GastoDto gastoDto)
        {
            Gasto gasto = _context.Gastos.Where(gastoUsuario => gastoUsuario.UsuarioId == usuarioId)
                                         .FirstOrDefault(g => g.Id == id);
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
                                         .FirstOrDefault(g => g.Id == id);
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
