namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        #region Constructors

        public ApplicationUserConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Application
            HasRequired(x => x.Application)
                .WithMany(x => x.ApplicationUsers)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);

            //ApplicationId
            Property(x => x.ApplicationId)
                .HasColumnOrder(1);

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(2)
                .IsRequired();
        }

        #endregion Constructors
    }
}