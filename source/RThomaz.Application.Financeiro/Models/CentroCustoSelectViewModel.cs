using System.Collections.Generic;

namespace RThomaz.Application.Financeiro.Models
{
    public class CentroCustoSelectViewModel
    {
        private readonly long _centroCustoId;
        private readonly string _nome;
        private readonly List<CentroCustoSelectViewModel> _children;

        public CentroCustoSelectViewModel
            (
                  long centroCustoId
                , string nome
                , List<CentroCustoSelectViewModel> children
            )
        {
            _centroCustoId = centroCustoId;
            _nome = nome;
            _children = children;
        }

        public long CentroCustoId { get { return _centroCustoId; } }
        public string Nome { get { return _nome; } }
        public List<CentroCustoSelectViewModel> Children { get { return _children; } }
    }
}