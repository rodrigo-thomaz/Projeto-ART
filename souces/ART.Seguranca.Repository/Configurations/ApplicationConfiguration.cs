namespace ART.Seguranca.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Seguranca.Repository.Entities;

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
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Description
            Property(x => x.Description)
                .HasMaxLength(5000)
                .IsOptional();
        }

        #endregion Constructors
    }
}