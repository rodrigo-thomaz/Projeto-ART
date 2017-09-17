using System;
using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class PessoaHomePage
    {
        #region Primitive Properties

        public long PessoaHomePageId { get; set; }

        public Guid AplicacaoId { get; set; }

        public long PessoaId { get; set; }

        public TipoPessoa TipoPessoa { get; set; }   

        public Guid TipoHomePageId { get; set; }

        public string HomePage { get; set; }
        
        #endregion

        #region Navigation Properties

        public Pessoa Pessoa { get; set; }

        public TipoHomePage TipoHomePage { get; set; }

        #endregion
    }
}