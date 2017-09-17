using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class MovimentoImportacaoConfiguration : EntityTypeConfiguration<MovimentoImportacao>
    {
        public MovimentoImportacaoConfiguration()
        {
            //Primary Keys
            HasKey(x => new 
            {
                x.MovimentoImportacaoId,
            });

            Property(x => x.MovimentoImportacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany()
                .WillCascadeOnDelete(false);

            //ImportadoEm
            Property(x => x.ImportadoEm)
                .IsRequired();
        }
    }
}
