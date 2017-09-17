using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class CentroCusto
    {
        #region Primitive Properties

        public long CentroCustoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public string Nome { get; set; }
        
        public bool Ativo { get; set; }

        public string Descricao { get; set; }

        public long? ResponsavelId { get; set; }        

        public long? ParentId { get; set; }

        #endregion

        #region Navigation Properties   
     
        public Aplicacao Aplicacao { get; set; }

        public CentroCusto Parent { get; set; }

        public ICollection<CentroCusto> Children { get; set; }

        public Usuario Responsavel { get; set; }

        public ICollection<LancamentoRateio> LancamentoRateios { get; set; }

        public ICollection<ProgramacaoRateio> ProgramacaoRateios { get; set; }

        #endregion
    }
}