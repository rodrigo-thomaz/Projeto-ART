using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Transferencia
    {
        #region Primitive Properties

        public long TransferenciaId { get; set; }

        public Guid AplicacaoId { get; set; }

        public long? TransferenciaProgramadaId { get; set; }

        public string Historico { get; set; }

        public string Numero { get; set; }

        public DateTime DataVencimento { get; set; }

        public decimal ValorVencimento { get; set; }
        
        public string Observacao { get; set; }

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public ICollection<Lancamento> Lancamentos { get; set; }

        public TransferenciaProgramada TransferenciaProgramada { get; set; }

        #endregion
    }
}