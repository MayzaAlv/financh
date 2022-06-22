using financh_backend.Data.Requests;
using financh_backend.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace financh_backend.Services
{
    public class LoginService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogarUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager.PasswordSignInAsync
                        (request.Username, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager.UserManager
                    .Users
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == request.Username.ToUpper());
                Token token = _tokenService.CreateToken(identityUser);

                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }

        public Result SolicitarResetSenha(SolicitarSenhaResetRequest request)
        {
           CustomIdentityUser identityUser = ProcurarEmail(request.Email);
           if(identityUser != null)
           {
                string codigoRecuperacao = _signInManager.UserManager
                    .GeneratePasswordResetTokenAsync(identityUser).Result;
                return Result.Ok().WithSuccess(codigoRecuperacao);
            }
            return Result.Fail("Falha na solicitação de redefinir senha");
        }

        internal Result ResetSenha(ResetSenhaRequest request)
        {
            CustomIdentityUser identityUser = ProcurarEmail(request.Email);
            IdentityResult resultadoIdentity = _signInManager.UserManager
                 .ResetPasswordAsync(identityUser, request.Token, request.Password).Result;
            if (resultadoIdentity.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao resetar a senha");
        }

        public CustomIdentityUser ProcurarEmail(string email)
        {
            return _signInManager.UserManager.Users
                .FirstOrDefault(usuario => usuario.NormalizedEmail == email.ToUpper());
        }
    }
}
