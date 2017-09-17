using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoConciliadoEditDTO
    {
        private readonly long _movimentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _observacao;

        public MovimentoConciliadoEditDTO
            (
                  long movimentoId
                , TipoTransacao tipoTransacao
                , string observacao
            )
        {
            _movimentoId = movimentoId;
            _tipoTransacao = tipoTransacao;
            _observacao = observacao;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string Observacao { get { return _observacao; } }        
    }
}