using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TipoEnderecoSelectViewDTO
    {
        private readonly Guid _tipoEnderecoId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;

        public TipoEnderecoSelectViewDTO
            (
                  Guid tipoEnderecoId
                , TipoPessoa tipoPessoa
                , string nome
            )
        {
            _tipoEnderecoId = tipoEnderecoId;
            _tipoPessoa = tipoPessoa;
            _nome = nome;
        }

        public Guid TipoEnderecoId { get { return _tipoEnderecoId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
    }
}