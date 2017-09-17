using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class PessoaJuridica : Pessoa
    {
        #region constructors

        public PessoaJuridica()
        {
            TipoPessoa = TipoPessoa.PessoaJuridica;
        }

        #endregion

        #region Primitive Properties

        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }

        #endregion

        #region Navigation Properties

        #endregion
    }
}
