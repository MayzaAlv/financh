using AutoMapper;
using financh_backend.Data.Dtos;
using financh_backend.Data.Requests;
using financh_backend.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace financh_backend.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);

            if (resultadoIdentity.Result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                var encoded = HttpUtility.UrlEncode(code);

                _emailService.EnviarEmail(new[] { usuarioIdentity.Email }, 
                    "Link de ativação", usuarioIdentity.Id, new[] { usuarioIdentity.UserName }, encoded);

                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar o usuário");
        }

        public Result AtivarConta(AtivarContaRequest request)
        {
            var usuarioIdentity = _userManager
                .Users
                .FirstOrDefault(usuario => usuario.Id == request.Id);
            var resultadoIdentity = _userManager
                .ConfirmEmailAsync(usuarioIdentity, request.CodigoAtivacao);

            if (resultadoIdentity.Result.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao autenticar usuário");
        }
    }
}
