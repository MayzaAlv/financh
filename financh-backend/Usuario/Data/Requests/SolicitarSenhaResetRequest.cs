using System.ComponentModel.DataAnnotations;

namespace financh_backend.Data.Requests
{
    public class SolicitarSenhaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
