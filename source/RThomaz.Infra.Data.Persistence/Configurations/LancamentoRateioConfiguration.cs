using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class LancamentoRateioConfiguration : EntityTypeConfiguration<LancamentoRateio>
    {
        public LancamentoRateioConfiguration()
        {
            //Primary Keys
            HasKey(x => new 
            { 
                x.LancamentoId,
                x.TipoTransacao,
                x.PlanoContaId,
                x.CentroCustoId,
            });

            //Foreing Keys            
            HasRequired(x => x.Lancamento)
                .WithMany(x => x.Rateios)
                .HasForeignKey(x => new
                {
                    x.LancamentoId,
                    x.TipoTransacao,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasRequired(x => x.PlanoConta)
                .WithMany(x => x.LancamentoRateios)
                .HasForeignKey(x => new 
                {
                    x.PlanoContaId,
                    x.TipoTransacao,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasRequired(x => x.CentroCusto)
                .WithMany(x => x.LancamentoRateios)
                .HasForeignKey(x => new
                {
                    x.CentroCustoId,
                })
                .WillCascadeOnDelete(false);            

            //Porcentagem
            Property(x => x.Porcentagem)
                .IsRequired()
                .HasPrecision(18, 2);            

            //Observacao
            Property(x => x.Observacao)
                .HasMaxLength(4000)
                .IsOptional();
        }
    }
}
