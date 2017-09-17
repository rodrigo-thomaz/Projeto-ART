using RThomaz.Domain.Financeiro.Enums;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoTelefoneDetailInsertModel
    {
        /// <summary>
        /// Nome do tipo de telefone
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Tipo de pessoa do tipo de telefone
        /// </summary>
        [Required]
        public TipoPessoa TipoPessoa { get; set; }

        /// <summary>
        /// Status do tipo de telefone
        /// </summary>
        [Required]
        public bool Ativo { get; set; }
    }
}