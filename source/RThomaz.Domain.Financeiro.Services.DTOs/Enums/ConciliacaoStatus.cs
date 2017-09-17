using System.ComponentModel;

namespace RThomaz.Domain.Financeiro.Services.DTOs.Enums
{
    public enum ConciliacaoStatus : byte
    {
        [Description("Não conciliado")]
        NaoConciliado = 0,

        [Description("Parcialmente")]
        Parcialmente = 1,

        [Description("Conciliado")]
        Conciliado = 2,
    }
}
