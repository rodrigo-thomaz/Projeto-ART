using System;
using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class PessoaTelefone
    {
        #region Primitive Properties

        public long PessoaTelefoneId { get; set; }

        public Guid AplicacaoId { get; set; }

        public long PessoaId { get; set; }

        public TipoPessoa TipoPessoa { get; set; }   

        public Guid TipoTelefoneId { get; set; }

        public string Telefone { get; set; }
        
        #endregion

        #region Navigation Properties

        public Pessoa Pessoa { get; set; }

        public TipoTelefone TipoTelefone { get; set; }

        #endregion
    }
}