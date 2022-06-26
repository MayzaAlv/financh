using System.ComponentModel.DataAnnotations;

namespace Operacoes.Data.Dtos.Salario
{
    public class SalarioDto
    {
        [Required]
        public double SalarioFixo { get; set; }
        [Required]
        public double HoraExtra { get; set; }
        [Required]
        public double GanhoOcasional { get; set; }
        [Required]
        public DateTime DataSalario { get; set; }
    }
}
