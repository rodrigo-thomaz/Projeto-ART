using System;
using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class LancamentoRateio
    {
        #region Primitive Properties

        public Guid LancamentoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public long PlanoContaId { get; set; }

        public long CentroCustoId { get; set; }

        public string Observacao { get; set; }

        public decimal Porcentagem { get; set; }

        #endregion

        #region Navigation Properties

        public Lancamento Lancamento { get; set; }

        public PlanoConta PlanoConta { get; set; }

        public CentroCusto CentroCusto { get; set; }

        #endregion
    }
}