using AutoMapper;
using Operacoes.Data.Dtos.Gasto;
using Operacoes.Data.Dtos.Salario;
using Operacoes.Models;

namespace Operacoes.Profiles
{
    public class GastoProfile : AutoMapper.Profile
    {
        public GastoProfile()
        {
            CreateMap<GastoDto, Gasto>();
            CreateMap<Gasto, GastoDto>();
            CreateMap<Gasto, GastoIdDto>();
        }
    }
}
