namespace ART.Seguranca.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Seguranca.Repository.Entities;

    public class ApplicationRoleConfiguration : EntityTypeConfiguration<ApplicationRole>
    {
        #region Constructors

        public ApplicationRoleConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
        }

        #endregion Constructors
    }
}