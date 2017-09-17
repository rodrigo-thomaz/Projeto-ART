namespace RThomaz.Application.Financeiro.Models
{
    public class CentroCustoDetailViewModel
    {
        private readonly long _centroCustoId;
        private readonly CentroCustoSelectViewModel _parent;
        private readonly UsuarioSelectViewModel _responsavel;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _descricao;

        public CentroCustoDetailViewModel
            (
                  long centroCustoId
                , CentroCustoSelectViewModel parent
                , UsuarioSelectViewModel responsavel
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
        public CentroCustoSelectViewModel Parent { get { return _parent; } }
        public UsuarioSelectViewModel Responsavel { get { return _responsavel; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}