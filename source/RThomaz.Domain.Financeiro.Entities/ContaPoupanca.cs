using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class ContaPoupanca : Conta
    {
        #region constructors

        public ContaPoupanca()
        {
            TipoConta = TipoConta.ContaPoupanca;
        }

        #endregion

        #region Primitive Properties

        public long BancoId { get; set; }

        public DadoBancario DadoBancario { get; set; }

        public SaldoInicial SaldoInicial { get; set; }
        
        #endregion

        #region Navigation Properties

        public Banco Banco { get; set; }

        #endregion

        #region NotMapped Properties

        public string NomeExibicao
        {
            get
            {
                string result;

                if(Banco == null)
                {
                    result = string.Format("Ag: {0} C/C: {1}", DadoBancario.NumeroAgencia, DadoBancario.NumeroConta);
                }
                else
                {
                    result = string.Format("{0} ({1}) Ag: {2} C/P: {3}", Banco.NomeAbreviado, Banco.Numero, DadoBancario.NumeroAgencia, DadoBancario.NumeroConta);
                }
                
                return result;
            }
        }

        #endregion
    }
}