namespace RThomaz.Business.DTOs
{
    public class ConciliacaoSaveDTO
    {
        private readonly long _movimentoId;
        private readonly decimal _valorConciliado;

        public ConciliacaoSaveDTO
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
