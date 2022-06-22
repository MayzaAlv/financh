using System.ComponentModel.DataAnnotations;

namespace Operacoes.Models
{
    public class Gasto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double GastoFixo { get; set; }
        [Required]
        public double Divida { get; set; }
        [Required]
        public double GastoOcasional { get; set; }
        [Required]
        public DateTime DataGasto { get; set; }
        [Required]
        public int UsuarioId { get; set; }
    }
}
