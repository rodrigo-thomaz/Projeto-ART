using System.ComponentModel;

namespace RThomaz.Domain.Financeiro.Services.DTOs.Enums
{
    public enum ConciliacaoOrigem : byte
    {
        [Description("Importado")]
        Importado = 0,

        [Description("Manual")]
        Manual = 1,
    }
}
