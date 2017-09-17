using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TipoEmailSelectViewDTO
    {
        private readonly Guid _tipoEmailId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;

        public TipoEmailSelectViewDTO
            (
                  Guid tipoEmailId
                , TipoPessoa tipoPessoa
                , string nome
            )
        {
            _tipoEmailId = tipoEmailId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
        }

        public Guid TipoEmailId { get { return _tipoEmailId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
    }
}