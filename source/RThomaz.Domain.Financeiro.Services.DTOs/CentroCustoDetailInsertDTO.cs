namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class CentroCustoDetailInsertDTO
    {
        private readonly long? _parentId;
        private readonly long? _responsavelId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _descricao;

        public CentroCustoDetailInsertDTO
            (
                  long? parentId
                , long? responsavelId
                , string nome
                , bool ativo
                , string descricao
            )
        {
            _parentId = parentId;
            _responsavelId = responsavelId;
            _nome = nome;
            _ativo = ativo;
            _descricao = descricao;
        }

        public long? ParentId { get { return _parentId; } }
        public long? ResponsavelId { get { return _responsavelId; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}
