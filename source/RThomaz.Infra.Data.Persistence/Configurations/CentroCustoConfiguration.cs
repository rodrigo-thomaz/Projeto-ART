using System.Data.Entity.ModelConfiguration;
using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class CentroCustoConfiguration : EntityTypeConfiguration<CentroCusto>
    {
        public CentroCustoConfiguration()
        {
           //Primary Keys
            HasKey(x => new
            {
                x.CentroCustoId,
            });

            //CentroCustoId
            Property(x => x.CentroCustoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany()
                .WillCascadeOnDelete(false);

            //Nome
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(250);

            //Descrição
            Property(x => x.Descricao)
                .IsOptional()
                .HasMaxLength(4000);

            //Foreing Keys  
            HasOptional(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => new
                {
                    x.ParentId,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys  
            HasOptional(x => x.Responsavel)
                .WithMany(x => x.Responsaveis)
                .HasForeignKey(x => new 
                {
                    x.ResponsavelId,
                })
                .WillCascadeOnDelete(false);
        }
    }
}
