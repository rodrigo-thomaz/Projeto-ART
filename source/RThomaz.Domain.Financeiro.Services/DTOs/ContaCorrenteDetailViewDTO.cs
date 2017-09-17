namespace RThomaz.Business.DTOs
{
    public class ContaCorrenteDetailDTO
    {
        private readonly long _contaId;
        private readonly BancoSelectViewDTO _bancoSelectView;
        private readonly DadoBancarioDTO _dadoBancario;
        private readonly string _descricao;
        private readonly bool _ativo;
        private readonly SaldoInicialDTO _saldoInicial;

        public ContaCorrenteDetailDTO
            (
                  long contaId
                , BancoSelectViewDTO bancoSelectView
                , DadoBancarioDTO dadoBancario
                , string descricao
                , bool ativo
                , SaldoInicialDTO saldoInicial
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
        public BancoSelectViewDTO BancoSelectView { get { return _bancoSelectView; } }
        public DadoBancarioDTO DadoBancario { get { return _dadoBancario; } }
        public string Descricao { get { return _descricao; } }
        public bool Ativo { get { return _ativo; } }
        public SaldoInicialDTO SaldoInicial { get { return _saldoInicial; } }
    }
}