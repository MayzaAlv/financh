using Microsoft.AspNetCore.Identity;

namespace financh_backend.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DataNascimento { get; set; }
    }
}
