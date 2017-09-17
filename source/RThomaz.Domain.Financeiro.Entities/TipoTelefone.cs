using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class TipoTelefone
    {
        #region Primitive Properties

        public Guid TipoTelefoneId { get; set; }

        public Guid AplicacaoId { get; set; }

        public string Nome { get; set; }

        public TipoPessoa TipoPessoa { get; set; }

        public bool Ativo { get; set; }

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public ICollection<PessoaTelefone> PessoaTelefones { get; set; }        

        #endregion
    }
}
