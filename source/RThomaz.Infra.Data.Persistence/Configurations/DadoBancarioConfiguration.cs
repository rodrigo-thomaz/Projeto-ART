using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class DadoBancarioConfiguration : ComplexTypeConfiguration<DadoBancario>
    {
        public DadoBancarioConfiguration()
        {
            //NumeroAgencia
            Property(x => x.NumeroAgencia)
                .IsRequired()
                .HasColumnName("NumeroAgencia")
                .HasMaxLength(20);

            //NumeroConta
            Property(x => x.NumeroConta)
                .IsRequired()
                .HasColumnName("NumeroConta")
                .HasMaxLength(20);
        }
    }
}
