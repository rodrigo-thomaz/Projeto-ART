using RThomaz.Domain.Financeiro.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoEmailDetailViewModel
    {
        private readonly Guid _tipoEmailId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoEmailDetailViewModel
            (
                  Guid tipoEmailId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _tipoEmailId = tipoEmailId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        /// <summary>
        /// Id do tipo de email
        /// </summary>
        [Required]
        public Guid TipoEmailId { get { return _tipoEmailId; } }

        /// <summary>
        /// Nome do tipo de email
        /// </summary>
        [Required]
        public string Nome { get { return _nome; } }

        /// <summary>
        /// Tipo de pessoa do tipo de email
        /// </summary>
        [Required]
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }

        /// <summary>
        /// Status do tipo de email
        /// </summary>
        [Required]
        public bool Ativo { get { return _ativo; } }
    }
}