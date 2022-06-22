using System.ComponentModel.DataAnnotations;

namespace Operacoes.Data.Dtos.Gasto
{
    public class GastoDto
    {
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
