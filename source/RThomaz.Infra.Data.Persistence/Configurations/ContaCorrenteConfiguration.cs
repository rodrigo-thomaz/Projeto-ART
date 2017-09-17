using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class ContaCorrenteConfiguration : EntityTypeConfiguration<ContaCorrente>
    {
        public ContaCorrenteConfiguration()
        {
            ToTable("ContaCorrente");

            //Primary Keys
            HasKey(x => new
            {
                x.ContaId,
                x.TipoConta,
            });

            //ContaId
            Property(x => x.ContaId)
                .IsRequired();

            //TipoConta
            Property(x => x.TipoConta)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //BancoId
            HasRequired(x => x.Banco)
                .WithMany(x => x.ContasCorrente)
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
