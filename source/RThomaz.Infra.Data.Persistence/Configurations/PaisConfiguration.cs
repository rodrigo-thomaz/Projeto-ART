using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PaisConfiguration : EntityTypeConfiguration<Pais>
    {
        public PaisConfiguration()
        {
           //Primary Keys
            HasKey(x => x.PaisId);

            //Nome
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            //ISO2
            Property(x => x.ISO2)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            //ISO3
            Property(x => x.ISO3)
                .IsOptional()
                .IsFixedLength()
                .HasMaxLength(3);

            //NumCode
            Property(x => x.NumCode)
                .IsOptional()
                .HasMaxLength(4);

            //DDI
            Property(x => x.DDI)
                .IsOptional()
                .HasMaxLength(10);

            //ccTLD
            Property(x => x.ccTLD)
                .IsOptional()
                .HasMaxLength(5);

            //BandeiraStorageObject
            Property(x => x.BandeiraStorageObject)
                .IsOptional()
                .HasMaxLength(255);
        }
    }
}
