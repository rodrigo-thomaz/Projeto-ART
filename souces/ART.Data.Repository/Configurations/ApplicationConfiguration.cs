namespace ART.Data.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Data.Repository.Entities;

    public class ApplicationConfiguration : EntityTypeConfiguration<Application>
    {
        #region Constructors

        public ApplicationConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            // Name
            Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            //Description
            Property(x => x.Description)
                .HasMaxLength(5000)
                .IsOptional();
        }

        #endregion Constructors
    }
}