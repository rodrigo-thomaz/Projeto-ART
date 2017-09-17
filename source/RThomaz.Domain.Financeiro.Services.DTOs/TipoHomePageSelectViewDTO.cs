using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TipoHomePageSelectViewDTO
    {
        private readonly Guid _tipoHomePageId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;

        public TipoHomePageSelectViewDTO
            (
                  Guid tipoHomePageId
                , TipoPessoa tipoPessoa
                , string nome
            )
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