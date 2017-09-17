using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class LancamentoPagoRecebidoDetailEditModel
    {
        public long LancamentoId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public string Observacao { get; set; }
    }
}