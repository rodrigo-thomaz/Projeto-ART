using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class TipoEmailSelectViewModel
    {
        private readonly Guid _tipoEmailId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly string _nome;

        public TipoEmailSelectViewModel(Guid tipoEmailId, TipoPessoa tipoPessoa, string nome)
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