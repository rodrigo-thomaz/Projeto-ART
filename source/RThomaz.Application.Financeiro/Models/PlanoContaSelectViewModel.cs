using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Application.Financeiro.Models
{
    public class PlanoContaSelectViewModel
    {
        private readonly long _planoContaId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly List<PlanoContaSelectViewModel> _children;
        private readonly string _nome;

        public PlanoContaSelectViewModel
            (
                  long planoContaId
                , TipoTransacao tipoTransacao
                , List<PlanoContaSelectViewModel> children
                , string nome
            )
        {
            _planoContaId = planoContaId;
            _tipoTransacao = tipoTransacao;
            _children = children;
            _nome = nome;
        }

        public long PlanoContaId { get { return _planoContaId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public List<PlanoContaSelectViewModel> Children { get { return _children; } }
        public string Nome { get { return _nome; } }
    }
}