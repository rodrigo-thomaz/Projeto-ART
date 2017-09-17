using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class ContaConfiguration : EntityTypeConfiguration<Conta>
    {
        public ContaConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.ContaId,
                x.TipoConta,
            });

            //ContaId
            Property(x => x.ContaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //TipoConta
            Property(x => x.TipoConta)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany()
                .WillCascadeOnDelete(false);

            //Descrição
            Property(x => x.Descricao)
                .IsOptional()
                .HasMaxLength(4000);
        }
    }
}
