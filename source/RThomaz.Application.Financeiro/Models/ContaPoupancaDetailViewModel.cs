namespace RThomaz.Application.Financeiro.Models
{
    public class ContaPoupancaDetailViewModel
    {
        private readonly long _contaId;
        private readonly BancoSelectViewModel _bancoSelectView;
        private readonly DadoBancarioModel _dadoBancario;
        private readonly string _descricao;
        private readonly bool _ativo;
        private readonly SaldoInicialModel _saldoInicial;

        public ContaPoupancaDetailViewModel
            (
                  long contaId
                , BancoSelectViewModel bancoSelectView
                , DadoBancarioModel dadoBancario
                , string descricao
                , bool ativo
                , SaldoInicialModel saldoInicial
            )
        {
            _contaId = contaId;
            _bancoSelectView = bancoSelectView;
            _dadoBancario = dadoBancario;
            _descricao = descricao;
            _ativo = ativo;
            _saldoInicial = saldoInicial;
        }

        public long ContaId { get { return _contaId; } }
        public BancoSelectViewModel BancoSelectView { get { return _bancoSelectView; } }
        public DadoBancarioModel DadoBancario { get { return _dadoBancario; } }
        public string Descricao { get { return _descricao; } }
        public bool Ativo { get { return _ativo; } }
        public SaldoInicialModel SaldoInicial { get { return _saldoInicial; } }
    }
}