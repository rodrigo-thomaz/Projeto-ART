using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class BandeiraCartaoConfiguration : EntityTypeConfiguration<BandeiraCartao>
    {
        public BandeiraCartaoConfiguration()
        {
           //Primary Keys
            HasKey(x => new
            {
                x.BandeiraCartaoId,
            });

            //BandeiraCartaoId
            Property(x => x.BandeiraCartaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Unique_Nome", 0) { IsUnique = true }));

            //Nome
            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Unique_Nome", 1) { IsUnique = true }));

            //LogoStorageObject
            Property(x => x.LogoStorageObject)
                .IsOptional()
                .HasMaxLength(255);
        }
    }
}
