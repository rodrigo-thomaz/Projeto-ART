using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class ContaEspecie : Conta
    {
        #region constructors

        public ContaEspecie()
        {
            TipoConta = TipoConta.ContaEspecie;
        }

        #endregion

        #region Primitive Properties
        
        public string Nome { get; set; }

        public SaldoInicial SaldoInicial { get; set; }
        
        #endregion                
    }
}