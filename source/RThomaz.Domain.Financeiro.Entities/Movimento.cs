using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Movimento
    {
        #region Primitive Properties

        public long MovimentoId { get; set; }
        public Guid AplicacaoId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public long ContaId { get; set; }
        public TipoConta TipoConta { get; set; }        
        public DateTime DataMovimento { get; set; }
        public decimal ValorMovimento { get; set; }
        public string Historico { get; set; }
        public long? MovimentoImportacaoId { get; set; }
        public string Observacao { get; set; }
        public bool EstaConciliado { get; set; }

        #endregion

        #region Navigation Properties       

        public Aplicacao Aplicacao { get; set; }
        public Conta Conta { get; set; }
        public MovimentoImportacao MovimentoImportacao { get; set; }
        public ICollection<Conciliacao> Conciliacoes { get; set; }

        #endregion
    }
}
