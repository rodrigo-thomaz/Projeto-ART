using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class TipoEndereco
    {
        #region Primitive Properties

        public Guid TipoEnderecoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public string Nome { get; set; }

        public TipoPessoa TipoPessoa { get; set; }

        public bool Ativo { get; set; }

        #endregion

        #region Navigation Properties        

        public Aplicacao Aplicacao { get; set; }

        public ICollection<PessoaEndereco> PessoaEnderecos { get; set; }

        #endregion
    }
}
