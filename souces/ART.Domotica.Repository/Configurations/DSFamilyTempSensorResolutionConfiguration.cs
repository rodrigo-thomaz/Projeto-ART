namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DSFamilyTempSensorResolutionConfiguration : EntityTypeConfiguration<DSFamilyTempSensorResolution>
    {
        #region Constructors

        public DSFamilyTempSensorResolutionConfiguration()
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

            //Bits
            Property(x => x.Bits)
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Resolution
            Property(x => x.Resolution)
                .HasColumnOrder(3)
                .HasPrecision(5,4);

            //ResolutionDecimalPlaces
            Property(x => x.DecimalPlaces)
                .IsRequired()
                .HasColumnOrder(4);

            //ConversionTime
            Property(x => x.ConversionTime)
                .HasColumnOrder(5)
                .HasPrecision(5, 2);

            //Description
            Property(x => x.Description)
                .HasColumnOrder(6)
                .HasMaxLength(5000)
                .IsOptional();
        }

        #endregion Constructors
    }
}