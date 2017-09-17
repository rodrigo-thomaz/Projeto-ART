using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class SaldoInicialConfiguration : ComplexTypeConfiguration<SaldoInicial>
    {
        public SaldoInicialConfiguration()
        {
            //Data
            Property(x => x.Data)
                .HasColumnName("SaldoInicialData")
                .IsRequired();

            //Valor
            Property(x => x.Valor)
                .IsRequired()
                .HasColumnName("SaldoInicialValor")
                .HasPrecision(18, 2);
        }
    }
}
