using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TipoEnderecoMasterDTO
    {
        private readonly Guid _tipoEnderecoId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;
        private readonly bool _ativo;

        public TipoEnderecoMasterDTO
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

        public Guid TipoEnderecoId { get { return _tipoEnderecoId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}