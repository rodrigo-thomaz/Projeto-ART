namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ConciliacaoDetailUpdateDTO
    {
        private readonly long _movimentoId;
        private readonly decimal _valorConciliado;

        public ConciliacaoDetailUpdateDTO
            (
                  long movimentoId
                , decimal valorConciliado
            )
        {
            _movimentoId = movimentoId;
            _valorConciliado = valorConciliado;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public decimal ValorConciliado { get { return _valorConciliado; } }
    }
}
