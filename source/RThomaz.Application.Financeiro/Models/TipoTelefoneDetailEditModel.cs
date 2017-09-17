using RThomaz.Domain.Financeiro.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoTelefoneDetailEditModel
    {
        /// <summary>
        /// Id do tipo de telefone
        /// </summary>
        [Required]
        public Guid TipoTelefoneId { get; set; }

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