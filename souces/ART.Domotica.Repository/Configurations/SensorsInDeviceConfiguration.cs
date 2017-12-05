namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorsInDeviceConfiguration : EntityTypeConfiguration<SensorsInDevice>
    {
        #region Constructors

        public SensorsInDeviceConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.DeviceSensorsId,
                x.SensorId,
            });

            //DeviceId
            Property(x => x.DeviceSensorsId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorId
            Property(x => x.SensorId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //DeviceSensors
            HasRequired(x => x.DeviceSensors)
                .WithMany(x => x.SensorsInDevice)
                .HasForeignKey(x => x.DeviceSensorsId)
                .WillCascadeOnDelete(false);

            //Sensor
            HasRequired(x => x.Sensor)
                .WithMany(x => x.SensorsInDevice)
                .HasForeignKey(x => x.SensorId)
                .WillCascadeOnDelete(false);

            //Ordination
            Property(x => x.Ordination)
                .HasColumnOrder(2)
                .IsRequired();
        }

        #endregion Constructors
    }
}