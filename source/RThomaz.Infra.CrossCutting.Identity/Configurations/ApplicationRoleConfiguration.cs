using RThomaz.Infra.CrossCutting.Identity.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.CrossCutting.Identity.Configurations
{
    public class ApplicationRoleConfiguration : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Application
            HasRequired(x => x.Application)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);
        }
    }
}
