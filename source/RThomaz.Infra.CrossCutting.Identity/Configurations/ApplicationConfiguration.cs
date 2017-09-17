using RThomaz.Infra.CrossCutting.Identity.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.CrossCutting.Identity.Configurations
{
    public class ApplicationConfiguration : EntityTypeConfiguration<Application>
    {
        public ApplicationConfiguration()
        {
            //Primary Keys
            HasKey(x => x.ApplicationId);

            Property(x => x.ApplicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //StorageBucketName
            Property(x => x.StorageBucketName)
                .IsRequired()
                .HasMaxLength(56);
        }
    }
}
