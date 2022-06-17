using System.ComponentModel.DataAnnotations;

namespace financh_backend.Data.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }    
    }
}
