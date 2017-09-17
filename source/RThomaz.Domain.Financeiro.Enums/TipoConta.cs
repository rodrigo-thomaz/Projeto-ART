using System.ComponentModel;

namespace RThomaz.Domain.Financeiro.Enums
{
    public enum TipoConta : byte
    {
        [Description("Dinheiro em espécie")]
        ContaEspecie = 0,

        [Description("Conta corrente")]
        ContaCorrente = 1,

        [Description("Conta poupança")]
        ContaPoupanca = 2,

        [Description("Cartão de crédito")]
        ContaCartaoCredito = 3,
    }
}
