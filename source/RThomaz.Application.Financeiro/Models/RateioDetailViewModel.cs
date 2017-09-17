namespace RThomaz.Application.Financeiro.Models
{
    public class RateioDetailViewModel
    {
        private readonly PlanoContaSelectViewModel _planoConta;
        private readonly CentroCustoSelectViewModel _centroCusto;
        private readonly string _observacao;
        private readonly decimal _porcentagem;

        public RateioDetailViewModel
            (
                  PlanoContaSelectViewModel planoConta
                , CentroCustoSelectViewModel centroCusto
                , string observacao
                , decimal porcentagem
            )
        {
            _planoConta = planoConta;
            _centroCusto = centroCusto;
            _observacao = observacao;
            _porcentagem = porcentagem;
        }

        public PlanoContaSelectViewModel PlanoConta { get { return _planoConta; } }
        public CentroCustoSelectViewModel CentroCusto { get { return _centroCusto; } }
        public string Observacao { get { return _observacao; } }
        public decimal Porcentagem { get { return _porcentagem; } }
    }
}