using System.ComponentModel;

namespace RThomaz.Domain.Financeiro.Enums
{
    public enum TipoProgramacao : byte
    {
        [Description("Lancamento programado")]
        LancamentoProgramado = 0,

        [Description("Lancamento parcelado")]
        LancamentoParcelado = 1,
    }
}
