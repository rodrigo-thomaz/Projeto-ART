namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ContaEspecieDetailInsertDTO
    {
        private readonly string _nome;
        private readonly string _descricao;
        private readonly bool _ativo;
        private readonly SaldoInicialDTO _saldoInicial;

        public ContaEspecieDetailInsertDTO
            (
                  string nome
                , string descricao
                , bool ativo
                , SaldoInicialDTO saldoInicial
            )
        {
            _nome = nome;
            _descricao = descricao;
            _ativo = ativo;
            _saldoInicial = saldoInicial;
        }

        public string Nome { get { return _nome; } }
        public string Descricao { get { return _descricao; } }
        public bool Ativo { get { return _ativo; } }
        public SaldoInicialDTO SaldoInicial { get { return _saldoInicial; } }
    }
}
