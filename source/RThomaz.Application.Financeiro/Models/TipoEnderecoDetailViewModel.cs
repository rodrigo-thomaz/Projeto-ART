using RThomaz.Domain.Financeiro.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoEnderecoDetailViewModel
    {
        private readonly Guid _tipoEnderecoId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoEnderecoDetailViewModel
            (
                  Guid tipoEnderecoId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _tipoEnderecoId = tipoEnderecoId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        /// <summary>
        /// Id do tipo de endereço
        /// </summary>
        [Required]
        public Guid TipoEnderecoId { get { return _tipoEnderecoId; } }

        /// <summary>
        /// Nome do tipo de endereço
        /// </summary>
        [Required]
        public string Nome { get { return _nome; } }

        /// <summary>
        /// Tipo de pessoa do tipo de endereço
        /// </summary>
        [Required]
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }

        /// <summary>
        /// Status do tipo de endereço
        /// </summary>
        [Required]
        public bool Ativo { get { return _ativo; } }
    }
}