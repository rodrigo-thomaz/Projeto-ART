namespace RThomaz.Business.DTOs
{
    public class PlanoContaEditDTO
    {
        private readonly long _planoContaId;
        private readonly long? _parentId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _descricao;

        public PlanoContaEditDTO
            (
                  long planoContaId
                , long? parentId
                , string nome
                , bool ativo
                , string descricao
            )
        {
            _planoContaId = planoContaId;
            _parentId = parentId;
            _nome = nome;
            _ativo = ativo;
            _descricao = descricao;
        }

        public long PlanoContaId { get { return _planoContaId; } }
        public long? ParentId { get { return _parentId; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}
