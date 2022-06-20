using AutoMapper;
using financh_backend.Data.Dtos;
using financh_backend.Models;
using Microsoft.AspNetCore.Identity;

namespace financh_backend.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
        }
    }
}
