using System;
using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class PessoaEndereco
    {
        #region Primitive Properties

        public long PessoaEnderecoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public long PessoaId { get; set; }

        public TipoPessoa TipoPessoa { get; set; }   

        public Guid TipoEnderecoId { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public long BairroId { get; set; }        

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        #endregion

        #region Navigation Properties

        public Pessoa Pessoa { get; set; }

        public Bairro Bairro { get; set; }

        public TipoEndereco TipoEndereco { get; set; }

        #endregion
    }
}