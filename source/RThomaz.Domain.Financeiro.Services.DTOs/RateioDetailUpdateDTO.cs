namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class RateioDetailUpdateDTO
    {
        private readonly long _planoContaId;
        private readonly long _centroCustoId;
        private readonly string _observacao;
        private readonly decimal _porcentagem;

        public RateioDetailUpdateDTO
            (
                  long planoContaId
                , long centroCustoId
                , string observacao
                , decimal porcentagem
            )
        {
            _planoContaId = planoContaId;
            _centroCustoId = centroCustoId;
            _observacao = observacao;
            _porcentagem = porcentagem;
        }

        public long PlanoContaId { get { return _planoContaId; } }
        public long CentroCustoId { get { return _centroCustoId; } }
        public string Observacao { get { return _observacao; } }
        public decimal Porcentagem { get { return _porcentagem; } }
    }
}
