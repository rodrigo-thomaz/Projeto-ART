using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LancamentoPagoRecebidoDetailEditDTO
    {
        private readonly long _lancamentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _observacao;

        public LancamentoPagoRecebidoDetailEditDTO
            (
                  long lancamentoId
                , TipoTransacao tipoTransacao
                , string observacao               
            )
        {
            _lancamentoId = lancamentoId;
            _tipoTransacao = tipoTransacao;
            _observacao = observacao;   
        }

        public long LancamentoId { get { return _lancamentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string Observacao { get { return _observacao; } }
    }
}
