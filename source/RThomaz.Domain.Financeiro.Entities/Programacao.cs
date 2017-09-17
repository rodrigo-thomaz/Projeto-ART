using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public abstract class Programacao
    {
        #region Primitive Properties

        public long ProgramacaoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public TipoProgramacao TipoProgramacao { get; protected set; }

        public TipoTransacao TipoTransacao { get; set; }

        public long? PessoaId { get; set; }

        public TipoPessoa? TipoPessoa { get; set; }

        public long ContaId { get; set; }

        public TipoConta TipoConta { get; set; }

        public Programador Programador { get; set; }        

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public Pessoa Pessoa { get; set; }

        public Conta Conta { get; set; }  

        public ICollection<Lancamento> Lancamentos { get; set; }

        public ICollection<ProgramacaoRateio> Rateios { get; set; }   

        #endregion
    }
}