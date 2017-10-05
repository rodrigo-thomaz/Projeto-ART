namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DSFamilyTempSensorConfiguration : EntityTypeConfiguration<DSFamilyTempSensor>
    {
        #region Constructors

        public DSFamilyTempSensorConfiguration()
        {
            ToTable("DSFamilyTempSensor");

            //Primary Keys
            HasKey(x => x.Id);

            //DeviceAddress
            Property(x => x.DeviceAddress)
                .HasMaxLength(15)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true } ));

            //Family
            Property(x => x.Family)
                .HasMaxLength(10)
                .IsRequired();

            //TemperatureScale
            HasRequired(x => x.TemperatureScale)
                .WithMany(x => x.DSFamilyTempSensors)
                .HasForeignKey(x => x.TemperatureScaleId)
                .WillCascadeOnDelete(false);

            //DSFamilyTempSensorResolution
            HasRequired(x => x.DSFamilyTempSensorResolution)
                .WithMany(x => x.DSFamilyTempSensors)
                .HasForeignKey(x => x.DSFamilyTempSensorResolutionId)
                .WillCascadeOnDelete(false);

            //HighAlarm
            Property(x => x.HighAlarm)
                .HasPrecision(6, 3);

            //LowAlarm
            Property(x => x.LowAlarm)
                .HasPrecision(6, 3);
        }

        #endregion Constructors
    }
}