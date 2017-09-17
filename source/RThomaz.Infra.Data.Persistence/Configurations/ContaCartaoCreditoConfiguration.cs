using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class ContaCartaoCreditoConfiguration : EntityTypeConfiguration<ContaCartaoCredito>
    {
        public ContaCartaoCreditoConfiguration()
        {
            ToTable("ContaCartaoCredito");

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

            //BandeiraCartaoId
            HasRequired(x => x.BandeiraCartao)
                .WithMany(x => x.ContasCartaoCredito)
                .HasForeignKey(x => new 
                {
                    x.BandeiraCartaoId,
                })
                .WillCascadeOnDelete(false);

            //ContaCorrenteId
            HasOptional(x => x.ContaCorrente)
                .WithMany(x => x.ContasCartaoCredito)
                .HasForeignKey(x => new
                {
                    x.ContaCorrente_ContaCorrenteId,
                    x.ContaCorrente_TipoConta,
                })
                .WillCascadeOnDelete(false);

            //Nome
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            //NomeExibicao
            Ignore(x => x.NomeExibicao);
        }
    }
}
