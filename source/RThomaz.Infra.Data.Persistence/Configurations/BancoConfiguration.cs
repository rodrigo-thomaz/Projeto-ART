using RThomaz.Domain.Financeiro.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class BancoConfiguration : EntityTypeConfiguration<Banco>
    {
        public BancoConfiguration()
        {
           //Primary Keys
            HasKey(x => new
            {
                x.BancoId,
            });

            //BancoId
            Property(x => x.BancoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>{
                        new IndexAttribute("IX_Unique_Banco_Nome", 0) { IsUnique = true },
                        new IndexAttribute("IX_Unique_Banco_NomeAbreviado", 0) { IsUnique = true },
                        new IndexAttribute("IX_Unique_Banco_Numero", 0) { IsUnique = true }
                }));

            //Nome
            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Unique_Banco_Nome", 1) { IsUnique = true }));

            //NomeAbreviado
            Property(t => t.NomeAbreviado)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Unique_Banco_NomeAbreviado", 1) { IsUnique = true }));

            //Numero
            Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Unique_Banco_Numero", 1) { IsUnique = true }));

            //CodigoImportacaoOfx
            Property(x => x.CodigoImportacaoOfx)
                .IsOptional()
                .HasMaxLength(15);

            //LogoStorageObject
            Property(x => x.LogoStorageObject)
                .IsOptional()
                .HasMaxLength(255);

            //Site
            Property(x => x.Site)
                .IsOptional()
                .HasMaxLength(500);

            //Descrição
            Property(x => x.Descricao)
                .IsOptional()
                .HasMaxLength(4000);

            //MascaraNumeroAgencia
            Property(x => x.MascaraNumeroAgencia)
                .IsOptional()
                .HasMaxLength(20);

            //MascaraNumeroContaCorrente
            Property(x => x.MascaraNumeroContaCorrente)
                .IsOptional()
                .HasMaxLength(20);

            //MascaraNumeroContaPoupanca
            Property(x => x.MascaraNumeroContaPoupanca)
                .IsOptional()
                .HasMaxLength(20);
        }
    }
}
