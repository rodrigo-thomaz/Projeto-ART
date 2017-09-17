namespace RThomaz.Business.DTOs
{
    public class CentroCustoDetailDTO
    {
        private readonly long _centroCustoId;
        private readonly long? _parentId;
        private readonly long? _responsavelId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _descricao;

        public CentroCustoDetailDTO
            (
                  long centroCustoId
                , long? parentId
                , long? responsavelId
                , string nome
                , bool ativo
                , string descricao
            )
        {
            _centroCustoId = centroCustoId;
            _parentId = parentId;
            _responsavelId = responsavelId;
            _nome = nome;
            _ativo = ativo;
            _descricao = descricao;
        }

        public long CentroCustoId { get { return _centroCustoId; } }
        public long? ParentId { get { return _parentId; } }
        public long? ResponsavelId { get { return _responsavelId; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}