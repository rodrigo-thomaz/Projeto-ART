namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class CentroCustoDetailViewDTO
    {
        private readonly long _centroCustoId;
        private readonly CentroCustoSelectViewDTO _parent;
        private readonly UsuarioSelectViewDTO _responsavel;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _descricao;

        public CentroCustoDetailViewDTO
            (
                  long centroCustoId
                , CentroCustoSelectViewDTO parent
                , UsuarioSelectViewDTO responsavel
                , string nome
                , bool ativo
                , string descricao
            )
        {
            _centroCustoId = centroCustoId;
            _parent = parent;
            _responsavel = responsavel;
            _nome = nome;
            _ativo = ativo;
            _descricao = descricao;
        }

        public long CentroCustoId { get { return _centroCustoId; } }
        public CentroCustoSelectViewDTO Parent { get { return _parent; } }
        public UsuarioSelectViewDTO Responsavel { get { return _responsavel; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}