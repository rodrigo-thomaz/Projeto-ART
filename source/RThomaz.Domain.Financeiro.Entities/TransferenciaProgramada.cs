using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class TransferenciaProgramada 
    {
        #region Primitive Properties

        public long TransferenciaProgramadaId { get; set; }

        public Guid AplicacaoId { get; set; }

        public Programador Programador { get; set; }

        public long ContaOrigem_ContaId { get; set; }

        public TipoConta ContaOrigem_TipoConta { get; set; }
        
        public long ContaDestino_ContaId { get; set; }

        public TipoConta ContaDestino_TipoConta { get; set; }

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public Conta ContaOrigem { get; set; }

        public Conta ContaDestino { get; set; }

        public ICollection<Transferencia> Transferencias { get; set; }

        public ICollection<Lancamento> Lancamentos { get; set; }

        #endregion
    }
}