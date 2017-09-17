using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class ContaPoupancaConfiguration : EntityTypeConfiguration<ContaPoupanca>
    {
        public ContaPoupancaConfiguration()
        {
            ToTable("ContaPoupanca");

            //Primary Keys
            HasKey(x => new
            {
                x.ContaId,
                x.TipoConta,
            });

            //ContaId
            Property(x => x.ContaId)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //TipoConta
            Property(x => x.TipoConta)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //BancoId
            HasRequired(x => x.Banco)
                .WithMany(x => x.ContasPoupanca)
                .HasForeignKey(x => new
                {
                    x.BancoId,
                })
                .WillCascadeOnDelete(false);

            //NomeExibicao
            Ignore(x => x.NomeExibicao);
        }
    }
}
