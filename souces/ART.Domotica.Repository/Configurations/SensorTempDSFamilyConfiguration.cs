namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorTempDSFamilyConfiguration : EntityTypeConfiguration<SensorTempDSFamily>
    {
        #region Constructors

        public SensorTempDSFamilyConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            // Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Sensor
            HasRequired(x => x.Sensor)
               .WithRequiredDependent(x => x.SensorTempDSFamily);

            //DeviceAddress
            Property(x => x.DeviceAddress)
                .HasColumnOrder(1)
                .HasMaxLength(32)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true } ));

            //Family
            Property(x => x.Family)
                .HasColumnOrder(2)
                .HasMaxLength(10)
                .IsRequired();

            //SensorTempDSFamilyResolution
            HasRequired(x => x.SensorTempDSFamilyResolution)
                .WithMany(x => x.SensorTempDSFamilies)
                .HasForeignKey(x => x.SensorTempDSFamilyResolutionId)
                .WillCascadeOnDelete(false);

            //SensorTempDSFamilyResolutionId
            Property(x => x.SensorTempDSFamilyResolutionId)
                .HasColumnOrder(3);
        }

        #endregion Constructors
    }
}