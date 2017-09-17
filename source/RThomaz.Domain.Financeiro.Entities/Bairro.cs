using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Bairro
    {
        #region Primitive Properties

        public long BairroId { get; set; }

        public long CidadeId { get; set; }

        public string Nome { get; set; }

        public string NomeAbreviado { get; set; }

        #endregion

        #region Navigation Properties

        public Cidade Cidade { get; set; }

        public ICollection<PessoaEndereco> PessoaEnderecos { get; set; }

        #endregion
    }
}
