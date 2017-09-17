using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class CidadeConfiguration : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfiguration()
        {
           //Primary Keys
            HasKey(x => x.CidadeId);

            //Nome
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(255);

            //NomeAbreviado
            Property(x => x.NomeAbreviado)
                .IsRequired()
                .HasMaxLength(255);          

            //Estado
            HasRequired(x => x.Estado)
                .WithMany(x => x.Cidades)
                .HasForeignKey(x => x.EstadoId)
                .WillCascadeOnDelete(false);
        }
    }
}
