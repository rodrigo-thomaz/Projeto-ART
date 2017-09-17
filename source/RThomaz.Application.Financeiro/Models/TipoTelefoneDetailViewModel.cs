using RThomaz.Domain.Financeiro.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoTelefoneDetailViewModel
    {
        private readonly Guid _tipoTelefoneId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoTelefoneDetailViewModel
            (
                  Guid tipoTelefoneId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _tipoTelefoneId = tipoTelefoneId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        /// <summary>
        /// Id do tipo de telefone
        /// </summary>
        [Required]
        public Guid TipoTelefoneId { get { return _tipoTelefoneId; } }

        /// <summary>
        /// Nome do tipo de telefone
        /// </summary>
        [Required]
        public string Nome { get { return _nome; } }

        /// <summary>
        /// Tipo de pessoa do tipo de telefone
        /// </summary>
        [Required]
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }

        /// <summary>
        /// Status do tipo de telefone
        /// </summary>
        [Required]
        public bool Ativo { get { return _ativo; } }
    }
}