using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class LancamentoParceladoConfiguration : EntityTypeConfiguration<LancamentoParcelado>
    {
        public LancamentoParceladoConfiguration()
        {
            ToTable("LancamentoParcelado");

            //Primary Keys
            HasKey(x => new
            {
                x.ProgramacaoId,
                x.TipoProgramacao,
                x.TipoTransacao,
            });

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoProgramacao
            Property(x => x.TipoProgramacao)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoTransacao
            Property(x => x.TipoTransacao)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
