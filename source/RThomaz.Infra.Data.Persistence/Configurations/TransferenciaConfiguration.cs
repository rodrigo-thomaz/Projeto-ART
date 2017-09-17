using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class TransferenciaConfiguration : EntityTypeConfiguration<Transferencia>
    {
        public TransferenciaConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.TransferenciaId,
            });

            //TransferenciaId
            Property(x => x.TransferenciaId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany()
                .WillCascadeOnDelete(false);            

            //ValorVencimento
            Property(x => x.ValorVencimento)
                .IsRequired()
                .HasPrecision(18, 2);

            //Data
            Property(x => x.DataVencimento)
                .IsRequired();

            //Historico
            Property(x => x.Historico)
                .HasMaxLength(500)
                .IsRequired();

            //Observacao
            Property(x => x.Observacao)
                .HasMaxLength(4000)
                .IsOptional();

            //Numero
            Property(x => x.Numero)
                .HasMaxLength(50)
                .IsOptional();

            //Foreing Keys            
            HasOptional(x => x.TransferenciaProgramada)
                .WithMany(x => x.Transferencias)
                .HasForeignKey(x => new
                {
                    x.TransferenciaProgramadaId,
                })
                .WillCascadeOnDelete(false);
        }
    }
}
