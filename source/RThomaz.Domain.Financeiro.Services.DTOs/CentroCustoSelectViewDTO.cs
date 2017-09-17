using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class CentroCustoSelectViewDTO
    {
        private readonly long _centroCustoId;
        private readonly string _nome;
        private readonly List<CentroCustoSelectViewDTO> _children;

        public CentroCustoSelectViewDTO
            (
                  long centroCustoId
                , string nome
                , List<CentroCustoSelectViewDTO> children
            )
        {
            _centroCustoId = centroCustoId;
            _nome = nome;
            _children = children;
        }

        public long CentroCustoId { get { return _centroCustoId; } }
        public string Nome { get { return _nome; } }
        public List<CentroCustoSelectViewDTO> Children { get { return _children; } }
    }
}