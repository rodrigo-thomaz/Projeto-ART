using RThomaz.Domain.Financeiro.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoEmailDetailEditModel
    {
        /// <summary>
        /// Id do tipo de email
        /// </summary>
        [Required]
        public Guid TipoEmailId { get; set; }

        /// <summary>
        /// Nome do tipo de email
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Tipo de pessoa do tipo de email
        /// </summary>
        [Required]
        public TipoPessoa TipoPessoa { get; set; }

        /// <summary>
        /// Status do tipo de email
        /// </summary>
        [Required]
        public bool Ativo { get; set; }
    }
}