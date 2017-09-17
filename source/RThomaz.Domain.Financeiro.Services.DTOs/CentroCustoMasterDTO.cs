namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class CentroCustoMasterDTO
    {
        private readonly long _centroCustoId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly long? _parentId;

        public CentroCustoMasterDTO
            (
                  long centroCustoId
                , string nome
                , bool ativo
                , long? parentId
            )
        {
            _centroCustoId = centroCustoId;
            _nome = nome;
            _ativo = ativo;
            _parentId = parentId;
        }

        public long CentroCustoId { get { return _centroCustoId; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public long? ParentId { get { return _parentId; } }
    }
}