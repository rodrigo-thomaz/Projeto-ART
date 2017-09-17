using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Lancamento
    {
        #region Primitive Properties

        public Guid LancamentoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public string Historico { get; set; }

        public string Numero { get; set; }

        public DateTime DataVencimento { get; set; }

        public decimal ValorVencimento { get; set; }   
                
        public string Observacao { get; set; }        

        public long? PessoaId { get; set; }

        public TipoPessoa? TipoPessoa { get; set; }

        public long ContaId { get; set; }

        public TipoConta TipoConta { get; set; }

        public long? TransferenciaId { get; set; }

        public long? TransferenciaProgramadaId { get; set; }

        public long? ProgramacaoId { get; set; }

        public TipoProgramacao? TipoProgramacao { get; set; }

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public Pessoa Pessoa { get; set; }

        public Conta Conta { get; set; }

        public Transferencia Transferencia { get; set; }

        public Programacao Programacao { get; set; }

        public TransferenciaProgramada TransferenciaProgramada { get; set; }

        public ICollection<LancamentoRateio> Rateios { get; set; }

        public Pagamento Pagamento { get; set; }

        #endregion
    }
}