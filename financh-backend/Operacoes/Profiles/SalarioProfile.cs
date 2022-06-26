using AutoMapper;
using Operacoes.Data.Dtos.Salario;
using Operacoes.Models;

namespace Operacoes.Profiles
{
    public class SalarioProfile : Profile
    {
        public SalarioProfile()
        { 
            CreateMap<SalarioDto, Salario>();
            CreateMap<Salario, SalarioDto>();
            CreateMap<Salario, SalarioIdDto>();
        }
    }
}
