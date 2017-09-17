using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public abstract class Conta
    {
        #region Primitive Properties
        
        public long ContaId { get; set; }

        public Guid AplicacaoId { get; set; }

        public TipoConta TipoConta { get; protected set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }
        
        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public ICollection<Lancamento> Lancamentos { get; set; }

        public ICollection<Programacao> Programacoes { get; set; }

        public ICollection<TransferenciaProgramada> TransferenciasProgramadasDeOrigem { get; set; }

        public ICollection<TransferenciaProgramada> TransferenciasProgramadasDeDestino { get; set; }

        public ICollection<Movimento> MovimentacoesFinanceiras { get; set; }
        
        #endregion        
    }
}