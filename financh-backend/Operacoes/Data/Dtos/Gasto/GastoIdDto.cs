using System.ComponentModel.DataAnnotations;

namespace Operacoes.Data.Dtos.Gasto
{
    public class GastoIdDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double GastoFixo { get; set; }
        [Required]
        public double Divida { get; set; }
        [Required]
        public double GastoOcasional { get; set; }
        [Required]
        public DateTime DataGasto { get; set; }
    }
}
