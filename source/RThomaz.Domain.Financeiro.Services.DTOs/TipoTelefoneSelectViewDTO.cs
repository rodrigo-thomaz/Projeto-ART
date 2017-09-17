using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TipoTelefoneSelectViewDTO
    {
        private readonly Guid _tipoTelefoneId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;

        public TipoTelefoneSelectViewDTO
            (
                  Guid tipoTelefoneId
                , TipoPessoa tipoPessoa
                , string nome
            )
        {
            _tipoTelefoneId = tipoTelefoneId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
        }

        public Guid TipoTelefoneId { get { return _tipoTelefoneId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
    }
}