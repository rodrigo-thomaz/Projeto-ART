using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class AplicacaoConfiguration : EntityTypeConfiguration<Aplicacao>
    {
        public AplicacaoConfiguration()
        {
            //Primary Keys
            HasKey(x => x.AplicacaoId);

            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            ////StorageBucketName
            //Property(x => x.StorageBucketName)
            //    .IsRequired()
            //    .HasMaxLength(56);
        }
    }
}
