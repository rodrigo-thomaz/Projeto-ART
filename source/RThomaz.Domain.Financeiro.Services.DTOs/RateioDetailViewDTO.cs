namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class RateioDetailViewDTO
    {
        private readonly PlanoContaSelectViewDTO _planoConta;
        private readonly CentroCustoSelectViewDTO _centroCusto;
        private readonly string _observacao;
        private readonly decimal _porcentagem;

        public RateioDetailViewDTO
            (
                  PlanoContaSelectViewDTO planoConta
                , CentroCustoSelectViewDTO centroCusto
                , string observacao
                , decimal porcentagem
            )
        {
            _planoConta = planoConta;
            _centroCusto = centroCusto;
            _observacao = observacao;
            _porcentagem = porcentagem;
        }

        public PlanoContaSelectViewDTO PlanoConta { get { return _planoConta; } }
        public CentroCustoSelectViewDTO CentroCusto { get { return _centroCusto; } }
        public string Observacao { get { return _observacao; } }
        public decimal Porcentagem { get { return _porcentagem; } }
    }
}
