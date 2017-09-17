using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class MovimentoConfiguration : EntityTypeConfiguration<Movimento>
    {
        public MovimentoConfiguration()
        {
           //Primary Keys
            HasKey(x => new 
            {
                x.MovimentoId,
                x.TipoTransacao,
            });

            Property(x => x.MovimentoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.AplicacaoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoTransacao
            Property(x => x.TipoTransacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Aplicacao
            HasRequired(u => u.Aplicacao)                
                .WithMany()
                .WillCascadeOnDelete(false);

            //Conta
            HasRequired(x => x.Conta)
                .WithMany(x => x.MovimentacoesFinanceiras)
                .HasForeignKey(x => new
                {
                    x.ContaId,
                    x.TipoConta,
                })
                .WillCascadeOnDelete(false);

            //ContaId
            Property(x => x.ContaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //ValorMovimento
            Property(x => x.ValorMovimento)
                .IsRequired()
                .HasPrecision(18, 2);

            //DataMovimento
            Property(x => x.DataMovimento)
                .IsRequired();

            //Historico
            Property(x => x.Historico)
                .HasMaxLength(500)
                .IsRequired();

            //MovimentoImportacaoId
            HasOptional(x => x.MovimentoImportacao)
                .WithMany(x => x.Movimentacoes)
                .HasForeignKey(x => new 
                {
                    x.MovimentoImportacaoId,
                })
                .WillCascadeOnDelete(false);

            //Observacao
            Property(x => x.Observacao)
                .HasMaxLength(4000)
                .IsOptional();

            //EstaConciliado
            Property(x => x.EstaConciliado)
                .IsRequired();
        }
    }
}
