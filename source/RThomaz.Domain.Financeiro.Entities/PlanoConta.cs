using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class PlanoConta
    {
        #region Primitive Properties

        public long PlanoContaId { get; set; }

        public Guid AplicacaoId { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }

        public long? ParentId { get; set; }

        #endregion

        #region Navigation Properties        

        public Aplicacao Aplicacao { get; set; }

        public PlanoConta Parent { get; set; }

        public ICollection<PlanoConta> Children { get; set; }

        public ICollection<LancamentoRateio> LancamentoRateios { get; set; }

        public ICollection<ProgramacaoRateio> ProgramacaoRateios { get; set; }

        #endregion
    }
}