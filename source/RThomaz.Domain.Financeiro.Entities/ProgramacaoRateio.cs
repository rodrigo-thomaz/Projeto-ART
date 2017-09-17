using System;
using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class ProgramacaoRateio
    {
        #region Primitive Properties

        public long ProgramacaoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public TipoProgramacao TipoProgramacao { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public long PlanoContaId { get; set; }

        public long CentroCustoId { get; set; }

        public string Observacao { get; set; }

        public decimal Porcentagem { get; set; }

        #endregion

        #region Navigation Properties

        public Programacao Programacao { get; set; }

        public PlanoConta PlanoConta { get; set; }

        public CentroCusto CentroCusto { get; set; }

        #endregion
    }
}