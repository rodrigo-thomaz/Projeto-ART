using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TipoTelefoneDetailEditDTO
    {
        private readonly Guid _tipoTelefoneId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoTelefoneDetailEditDTO
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

        public Guid TipoTelefoneId { get { return _tipoTelefoneId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}
