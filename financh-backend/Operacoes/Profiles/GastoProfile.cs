using AutoMapper;
using Operacoes.Data.Dtos.Gasto;
using Operacoes.Models;

namespace Operacoes.Profiles
{
    public class GastoProfile : Profile
    {
        public GastoProfile()
        {
            CreateMap<GastoDto, Gasto>();
            CreateMap<Gasto, GastoDto>();
        }
    }
}
