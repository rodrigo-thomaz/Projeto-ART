using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PagamentoConfiguration : EntityTypeConfiguration<Pagamento>
    {
        public PagamentoConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.LancamentoId,
                x.TipoTransacao,
            });

            //LancamentoId
            Property(x => x.LancamentoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //TipoTransacao
            Property(x => x.TipoTransacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            HasRequired(x => x.Lancamento)
                .WithRequiredDependent(x => x.Pagamento);

            //ValorPagamento
            Property(x => x.ValorPagamento)
                .IsRequired()
                .HasPrecision(18, 2);

            //DataPagamento
            Property(x => x.DataPagamento)
                .IsRequired();
        }
    }
}
