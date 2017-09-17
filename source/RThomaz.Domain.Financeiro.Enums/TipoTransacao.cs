using System.ComponentModel;

namespace RThomaz.Domain.Financeiro.Enums
{
    public enum TipoTransacao : byte
    {
        [Description("Crédito")]
        Credito = 0,

        [Description("Débito")]
        Debito = 1,
    }
}