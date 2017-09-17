using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class EstadoConfiguration : EntityTypeConfiguration<Estado>
    {
        public EstadoConfiguration()
        {
           //Primary Keys
            HasKey(x => x.EstadoId);

            //Nome
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(255);

            //Sigla
            Property(x => x.Sigla)
                .IsRequired()
                .HasMaxLength(255);

            //Pais
            HasRequired(x => x.Pais)
                .WithMany(x => x.Estados)
                .HasForeignKey(x => x.PaisId)
                .WillCascadeOnDelete(false);
        }
    }
}
