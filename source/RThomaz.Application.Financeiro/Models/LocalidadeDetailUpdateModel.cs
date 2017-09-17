using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class LocalidadeDetailUpdateModel
    {
        [Required]
        public string BairroNome { get; set; }

        [Required]
        public string BairroNomeAbreviado { get; set; }

        [Required]
        public string CidadeNome { get; set; }

        [Required]
        public string CidadeNomeAbreviado { get; set; }

        [Required]
        public string EstadoNome { get; set; }

        [Required]
        public string EstadoSigla { get; set; }

        [Required]
        public string PaisNome { get; set; }

        [Required]
        public string PaisISO2 { get; set; }
    }
}