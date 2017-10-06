namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class TemperatureScaleConfiguration : EntityTypeConfiguration<TemperatureScale>
    {
        #region Constructors

        public TemperatureScaleConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Name
            Property(x => x.Name)
                .HasColumnOrder(1)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Symbol
            Property(x => x.Symbol)
                .HasColumnOrder(2)
                .HasMaxLength(2)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Description
            Property(x => x.Description)
                .HasColumnOrder(3)
                .HasMaxLength(5000)
                .IsOptional();
        }

        #endregion Constructors
    }
}