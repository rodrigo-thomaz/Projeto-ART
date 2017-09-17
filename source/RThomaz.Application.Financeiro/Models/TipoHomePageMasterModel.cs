using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoHomePageMasterModel
    {
        private readonly Guid _tipoHomePageId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoHomePageMasterModel
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

        public Guid TipoHomePageId { get { return _tipoHomePageId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}