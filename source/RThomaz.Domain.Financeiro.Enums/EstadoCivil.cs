using System.ComponentModel;

namespace RThomaz.Domain.Financeiro.Enums
{
    public enum EstadoCivil : byte
    {
        [Description("Solteiro(a)")]
        Solteiro = 0,

        [Description("Casado(a)")]
        Casado = 1,

        [Description("Divorciado(a)")]
        Divorciado = 2,

        [Description("Viúvo(a)")]
        Viuvo = 3,

        [Description("Separado(a)")]
        Separado = 4,

        [Description("Companheiro(a)")]
        Companheiro = 5,
    }
}
