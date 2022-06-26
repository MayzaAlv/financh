using AutoMapper;
using FluentResults;
using Operacoes.Data;
using Operacoes.Data.Dtos.Salario;
using Operacoes.Models;

namespace Operacoes.Services
{
    public class SalarioService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SalarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SalarioIdDto AdicionarSalario(SalarioDto salarioDto, int usuarioId)
        {
            Salario salario = _mapper.Map<Salario>(salarioDto);
            salario.UsuarioId = usuarioId;
            _context.Salarios.Add(salario);
            _context.SaveChanges();

            salario = _context.Salarios.Where(salario => salario.UsuarioId == usuarioId)
                       .FirstOrDefault(s => s.DataSalario == salario.DataSalario);

            return _mapper.Map<SalarioIdDto>(salario);
        }

        public SalarioIdDto SalarioPorId(int salarioId)
        {
            Salario salario = _context.Salarios.FirstOrDefault(salarioUsuario => salarioUsuario.Id == salarioId);

            return _mapper.Map<SalarioIdDto>(salario);
        }

        public SalarioDto MostrarSalarioAtual(int usuarioId)
        {
            Salario salario = _context.Salarios.Where(salarioUsuario => salarioUsuario.UsuarioId == usuarioId)
                                .FirstOrDefault(s => s.DataSalario.Month == DateTime.Now.Month);

            if(salario != null)
            {
                return _mapper.Map<SalarioDto>(salario);
            }
            return null;
        }

        public SalarioDto MostrarSalarioMes(int usuarioId, int mes, int ano)
        {
            Salario salario = _context.Salarios.Where(salarioUsuario => salarioUsuario.UsuarioId == usuarioId)
                              .Where(sl => sl.DataSalario.Year == ano)
                              .FirstOrDefault(s => s.DataSalario.Month == mes);
            if(salario != null)
            {
                return _mapper.Map<SalarioDto>(salario);
            }
            return null;
        }

        public Result AtualizarSalario(int id, int usuarioId, SalarioDto salarioDto)
        {
            Salario salario = _context.Salarios.Where(salarioUsuario => salarioUsuario.UsuarioId == usuarioId)
                                               .FirstOrDefault(s => s.Id == id);
            if(salario == null)
            {
                return Result.Fail("Falha na procura");
            }
            _mapper.Map(salarioDto, salario);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletarSalario(int usuarioId, int id)
        {
            Salario salario = _context.Salarios.Where(salarioUsuario => salarioUsuario.UsuarioId == usuarioId)
                                               .FirstOrDefault(s => s.Id == id);
            if (salario == null)
            {
                return Result.Fail("Falha na procura");
            }
            _context.Remove(salario);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
