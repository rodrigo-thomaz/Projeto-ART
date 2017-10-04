namespace ART.Data.Repository.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using ART.Data.Repository.Entities;

    public class UserInApplicationConfiguration : EntityTypeConfiguration<UserInApplication>
    {
        #region Constructors

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

        #endregion Constructors
    }
}