using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class TipoEmailConfiguration : EntityTypeConfiguration<TipoEmail>
    {
        public TipoEmailConfiguration()
        {
           //Primary Keys
            HasKey(x => new 
            { 
                x.TipoEmailId,
                x.TipoPessoa,
            });

            //TipoEmailId
            Property(x => x.TipoEmailId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany()
                .WillCascadeOnDelete(false);

            Property(x => x.AplicacaoId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Unique_Nome", 0) { IsUnique = true }));

            //TipoPessoa
            Property(x => x.TipoPessoa)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Unique_Nome", 1) { IsUnique = true }));

            //Nome
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Unique_Nome", 2) { IsUnique = true }));
        }
    }
}
