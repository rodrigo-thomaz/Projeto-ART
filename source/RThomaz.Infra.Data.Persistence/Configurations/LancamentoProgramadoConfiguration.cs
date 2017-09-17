using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class LancamentoProgramadoConfiguration : EntityTypeConfiguration<LancamentoProgramado>
    {
        public LancamentoProgramadoConfiguration()
        {
            ToTable("LancamentoProgramado");

            //Primary Keys
            HasKey(x => new
            {
                x.ProgramacaoId,
                x.TipoProgramacao,
                x.TipoTransacao,
            });
            
            //TipoProgramacao
            Property(x => x.TipoProgramacao)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoTransacao
            Property(x => x.TipoTransacao)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);            
        }
    }
}
