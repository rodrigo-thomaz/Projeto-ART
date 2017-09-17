using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class ConciliacaoConfiguration : EntityTypeConfiguration<Conciliacao>
    {
        public ConciliacaoConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.MovimentoId,
                x.LancamentoId,
                x.TipoTransacao,
            });

            //MovimentoId
            Property(x => x.MovimentoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //LancamentoId
            Property(x => x.LancamentoId)
               .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //AplicacaoId
            Property(x => x.AplicacaoId)
               .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoTransacao
            Property(x => x.TipoTransacao)
               .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //MovimentoId
            HasRequired(x => x.Movimento)
                .WithMany(x => x.Conciliacoes)
                .HasForeignKey(x => new
                {
                    x.MovimentoId,
                    x.TipoTransacao,
                })
                .WillCascadeOnDelete(false);

            //LancamentoId
            HasRequired(x => x.Pagamento)
                .WithMany(x => x.Conciliacoes)
                .HasForeignKey(x => new
                {
                    x.LancamentoId,
                    x.TipoTransacao,
                })
                .WillCascadeOnDelete(false);

            //ValorConciliado
            Property(x => x.ValorConciliado)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}
