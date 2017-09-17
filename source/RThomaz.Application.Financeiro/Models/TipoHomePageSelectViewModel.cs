using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoHomePageSelectViewModel
    {
        private readonly Guid _tipoHomePageId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;

        public TipoHomePageSelectViewModel(Guid tipoHomePageId, TipoPessoa tipoPessoa, string nome)
        {
            _tipoHomePageId = tipoHomePageId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
        }

        public Guid TipoHomePageId { get { return _tipoHomePageId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
    }
}