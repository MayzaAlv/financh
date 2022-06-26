using System.ComponentModel.DataAnnotations;

namespace Operacoes.Data.Dtos.Salario
{
    public class SalarioIdDto
    {
        public int Id { get; set; }
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
