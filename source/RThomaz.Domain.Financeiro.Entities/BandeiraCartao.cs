using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class BandeiraCartao
    {
        #region Primitive Properties

        public long BandeiraCartaoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public string Nome { get; set; }

        public string LogoStorageObject { get; set; }
        
        public bool Ativo { get; set; }

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }        

        public ICollection<ContaCartaoCredito> ContasCartaoCredito { get; set; }

        #endregion
    }
}
