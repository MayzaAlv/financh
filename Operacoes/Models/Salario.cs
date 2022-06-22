using System.ComponentModel.DataAnnotations;

namespace Operacoes.Models
{
    public class Salario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double SalarioFixo { get; set; }
        [Required]
        public double HoraExtra { get; set; }
        [Required]
        public double GanhoOcasional { get; set; }
        [Required]
        public DateTime DataGasto { get; set; }
        [Required]
        public int UsuarioId { get; set; }
    }
}
