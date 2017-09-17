namespace RThomaz.Application.Financeiro.Models
{
    public class ContaEspecieDetailViewModel
    {
        private readonly long _contaId;
        private readonly string _nome;
        private readonly string _descricao;
        private readonly bool _ativo;
        private readonly SaldoInicialModel _saldoInicial;

        public ContaEspecieDetailViewModel
            (
                  long contaId
                , string nome
                , string descricao
                , bool ativo
                , SaldoInicialModel saldoInicial
            )
        {
            _contaId = contaId;
            _nome = nome;
            _descricao = descricao;
            _ativo = ativo;
            _saldoInicial = saldoInicial;
        }

        public long ContaId { get { return _contaId; } }
        public string Nome { get { return _nome; } }
        public string Descricao { get { return _descricao; } }
        public bool Ativo { get { return _ativo; } }
        public SaldoInicialModel SaldoInicial { get { return _saldoInicial; } }
    }
}