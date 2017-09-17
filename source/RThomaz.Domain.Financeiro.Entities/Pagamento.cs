using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Pagamento
    {
        #region Primitive Properties

        public Guid LancamentoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public DateTime DataPagamento { get; set; }

        public decimal ValorPagamento { get; set; }

        #endregion

        #region Navigation Properties

        public Lancamento Lancamento { get; set; }

        public ICollection<Conciliacao> Conciliacoes { get; set; }

        #endregion
    }
}
