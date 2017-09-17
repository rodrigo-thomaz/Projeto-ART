using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class PessoaFisica : Pessoa
    {
        #region constructors

        public PessoaFisica()
        {
            TipoPessoa = TipoPessoa.PessoaFisica;
        }

        #endregion

        #region Primitive Properties

        public string PrimeiroNome { get; set; }
        public string NomeDoMeio { get; set; }
        public string Sobrenome { get; set; }
        public Sexo Sexo { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string OrgaoEmissor { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Naturalidade { get; set; }
        public string Nacionalidade { get; set; }
        public int? CBOOcupacaoId { get; set; }
        public int? CBOSinonimoId { get; set; }

        #endregion

        #region NotMapped Properties

        public string NomeCompleto
        {
            get
            {
                var nomeCompleto = PrimeiroNome;

                if (!string.IsNullOrEmpty(NomeDoMeio))
                    nomeCompleto += " " + NomeDoMeio;

                if (!string.IsNullOrEmpty(Sobrenome))
                    nomeCompleto += " " + Sobrenome;

                return nomeCompleto;
            }
        }

        #endregion

        #region Navigation Properties

        public CBOOcupacao CBOOcupacao { get; set; }
        public CBOSinonimo CBOSinonimo { get; set; }

        #endregion
    }
}
