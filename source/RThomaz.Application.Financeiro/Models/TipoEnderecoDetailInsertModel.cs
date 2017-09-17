using RThomaz.Domain.Financeiro.Enums;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoEnderecoDetailInsertModel
    {
        /// <summary>
        /// Nome do tipo de endereço
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Tipo de pessoa do tipo de endereço
        /// </summary>
        [Required]
        public TipoPessoa TipoPessoa { get; set; }

        /// <summary>
        /// Status do tipo de endereço
        /// </summary>
        [Required]
        public bool Ativo { get; set; }
    }
}