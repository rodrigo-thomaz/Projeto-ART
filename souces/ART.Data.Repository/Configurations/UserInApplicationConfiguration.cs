using ART.Data.Repository.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Data.Repository.Configurations
{
    public class UserInApplicationConfiguration : EntityTypeConfiguration<UserInApplication>
    {
        public UserInApplicationConfiguration()
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