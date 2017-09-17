using System.ComponentModel;

namespace RThomaz.Domain.Financeiro.Enums
{
    public enum TipoPessoa : byte
    {
        [Description("Pessoa Física")]
        PessoaFisica = 0,

        [Description("Pessoa Jurídica")]
        PessoaJuridica = 1,
    }
}
