using ART.Seguranca.DistributedServices.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Seguranca.DistributedServices.Configurations
{
    public class UsersInApplicationConfiguration : EntityTypeConfiguration<UsersInApplication>
    {
        public UsersInApplicationConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.UserId,
                x.ApplicationId,
            });

            //Foreing Keys            
            HasRequired(x => x.User)
                .WithMany(x => x.UsersInApplication)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasRequired(x => x.Application)
                .WithMany(x => x.UsersInApplication)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);
        }
    }
}