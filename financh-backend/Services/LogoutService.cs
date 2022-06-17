using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace financh_backend.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _singInManager;

        public LogoutService(SignInManager<IdentityUser<int>> singInManager)
        {
            _singInManager = singInManager;
        }

        public Result DeslogarUsuario()
        {
            var resultadoIdentity = _singInManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }
            return Result.Fail("Logout falhou");
        }
    }
}
