using System;
using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class PessoaEmail
    {
        #region Primitive Properties

        public long PessoaEmailId { get; set; }

        public Guid AplicacaoId { get; set; }

        public long PessoaId { get; set; }

        public TipoPessoa TipoPessoa { get; set; }   

        public Guid TipoEmailId { get; set; }

        public string Email { get; set; }
        
        #endregion

        #region Navigation Properties

        public Pessoa Pessoa { get; set; }

        public TipoEmail TipoEmail { get; set; }

        #endregion
    }
}