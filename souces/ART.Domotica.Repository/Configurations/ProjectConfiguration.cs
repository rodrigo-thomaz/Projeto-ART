namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        #region Constructors

        public ProjectConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            // Name
            Property(x => x.Name)
                .HasColumnOrder(1)
                .HasMaxLength(255)
                .IsRequired();

            //Description
            Property(x => x.Description)
                .HasColumnOrder(2)
                .HasMaxLength(5000)
                .IsOptional();

            //Application
            HasRequired(x => x.Application)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);

            //ApplicationId
            Property(x => x.ApplicationId)
                .HasColumnOrder(3);

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(4)
                .IsRequired();

            //CreateByApplicationUserId
            Property(x => x.CreateByApplicationUserId)
                .HasColumnOrder(5);

            //CreateByApplicationUser
            HasRequired(x => x.CreateByApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.CreateByApplicationUserId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}