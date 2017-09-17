using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class BairroConfiguration : EntityTypeConfiguration<Bairro>
    {
        public BairroConfiguration()
        {
            //Primary Keys
            HasKey(x => x.BairroId);

            //Nome
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(255);

            //NomeAbreviado
            Property(x => x.NomeAbreviado)
                .IsRequired()
                .HasMaxLength(255);

            //Cidade
            HasRequired(x => x.Cidade)
                .WithMany(x => x.Bairros)
                .HasForeignKey(x => x.CidadeId)
                .WillCascadeOnDelete(false);
        }
    }
}
