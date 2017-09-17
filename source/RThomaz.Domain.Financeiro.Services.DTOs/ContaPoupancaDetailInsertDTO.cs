namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ContaPoupancaDetailInsertDTO
    {
        private readonly long _bancoId;
        private readonly DadoBancarioDTO _dadoBancario;
        private readonly string _descricao;
        private readonly bool _ativo;
        private readonly SaldoInicialDTO _saldoInicial;

        public ContaPoupancaDetailInsertDTO
            (
                  long bancoId
                , DadoBancarioDTO dadoBancario
                , string descricao
                , bool ativo
                , SaldoInicialDTO saldoInicial
            )
        {
            _bancoId = bancoId;
            _dadoBancario = dadoBancario;
            _descricao = descricao;
            _ativo = ativo;
            _saldoInicial = saldoInicial;
        }

        public long BancoId { get { return _bancoId; } }
        public DadoBancarioDTO DadoBancario { get { return _dadoBancario; } }
        public string Descricao { get { return _descricao; } }
        public bool Ativo { get { return _ativo; } }
        public SaldoInicialDTO SaldoInicial { get { return _saldoInicial; } }
    }
}
