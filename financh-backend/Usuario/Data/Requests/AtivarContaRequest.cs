using System.ComponentModel.DataAnnotations;

namespace financh_backend.Data.Requests
{
    public class AtivarContaRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CodigoAtivacao { get; set; }
    }
}
