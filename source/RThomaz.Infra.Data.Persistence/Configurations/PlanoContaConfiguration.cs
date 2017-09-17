using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PlanoContaConfiguration : EntityTypeConfiguration<PlanoConta>
    {
        public PlanoContaConfiguration()
        {
           //Primary Keys
            HasKey(x => new { 
                x.PlanoContaId,
                x.TipoTransacao,
            });

            //PlanoContaId
            Property(x => x.PlanoContaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoTransacao
            Property(x => x.TipoTransacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

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
                    x.TipoTransacao,
                })
                .WillCascadeOnDelete(false);
        }
    }
}
