using System;
using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Conciliacao
    {
        #region Primitive Properties
        
        public Guid LancamentoId { get; set; }
        public long MovimentoId { get; set; }
        public Guid AplicacaoId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal ValorConciliado { get; set; }

        #endregion

        #region Navigation Properties

        public Pagamento Pagamento { get; set; }
        public Movimento Movimento { get; set; }

        #endregion        
    }
}
