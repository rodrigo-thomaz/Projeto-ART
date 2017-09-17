using RThomaz.Domain.Financeiro.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoHomePageDetailViewModel
    {
        private readonly Guid _tipoHomePageId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoHomePageDetailViewModel
            (
                  Guid tipoHomePageId
                , TipoPessoa tipoPessoa
                , string nome
                , bool ativo
            )
        {
            _tipoHomePageId = tipoHomePageId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
            _ativo = ativo;
        }

        /// <summary>
        /// Id do tipo de home page
        /// </summary>
        [Required]
        public Guid TipoHomePageId { get { return _tipoHomePageId; } }

        /// <summary>
        /// Nome do tipo de home page
        /// </summary>
        [Required]
        public string Nome { get { return _nome; } }

        /// <summary>
        /// Tipo de pessoa do tipo de home page
        /// </summary>
        [Required]
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }

        /// <summary>
        /// Status do tipo de home page
        /// </summary>
        [Required]
        public bool Ativo { get { return _ativo; } }
    }
}