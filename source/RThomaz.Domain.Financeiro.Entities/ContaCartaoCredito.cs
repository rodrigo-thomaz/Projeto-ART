using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class ContaCartaoCredito : Conta
    {
        #region constructors

        public ContaCartaoCredito()
        {
            TipoConta = TipoConta.ContaCartaoCredito;
        }

        #endregion

        #region Primitive Properties

        public long BandeiraCartaoId { get; set; }

        public long? ContaCorrente_ContaCorrenteId { get; set; }        

        public TipoConta? ContaCorrente_TipoConta { get; protected set; }
                
        public string Nome { get; set; }

        #endregion   
 
        #region Navigation Properties

        public BandeiraCartao BandeiraCartao { get; set; }

        public ContaCorrente ContaCorrente { get; set; }

        #endregion

        #region NotMapped Properties

        public string NomeExibicao
        {
            get
            {
                string result;

                if (BandeiraCartao == null)
                {
                    result = Nome;
                }
                else
                {
                    result = string.Format("{0} - {1}", BandeiraCartao.Nome, Nome);
                }

                return result;
            }
        }

        #endregion
    }
}