using RThomaz.Database.Enums;

namespace RThomaz.Business.DTOs
{
    public class LancamentoPagoRecebidoEditDTO
    {
        private readonly long _lancamentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _observacao;

        public LancamentoPagoRecebidoEditDTO
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
