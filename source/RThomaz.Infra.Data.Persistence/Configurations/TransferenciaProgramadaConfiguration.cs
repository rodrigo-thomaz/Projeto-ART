using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class TransferenciaProgramadaConfiguration : EntityTypeConfiguration<TransferenciaProgramada>
    {
        public TransferenciaProgramadaConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.TransferenciaProgramadaId,
            });

            //TransferenciaProgramadaId
            Property(x => x.TransferenciaProgramadaId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany();

            //Foreing Keys            
            HasRequired(x => x.ContaOrigem)
                .WithMany(x => x.TransferenciasProgramadasDeOrigem)
                .HasForeignKey(x => new
                {
                    x.ContaOrigem_ContaId,
                    x.ContaOrigem_TipoConta,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasRequired(x => x.ContaDestino)
                .WithMany(x => x.TransferenciasProgramadasDeDestino)
                .HasForeignKey(x => new
                {
                    x.ContaDestino_ContaId,
                    x.ContaDestino_TipoConta,
                })
                .WillCascadeOnDelete(false);
        }
    }
}
