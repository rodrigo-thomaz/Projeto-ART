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

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .IsRequired();

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

            //HasLowAlarm
            Property(x => x.HasLowAlarm)
                .HasColumnOrder(3)
                .IsRequired();

            //HasHighAlarm
            Property(x => x.HasHighAlarm)
                .HasColumnOrder(4)
                .IsRequired();

            //LowAlarm
            Property(x => x.LowAlarm)
                .HasColumnOrder(5)
                .IsRequired()
                .HasPrecision(6, 3);

            //HighAlarm
            Property(x => x.HighAlarm)
                .HasColumnOrder(6)
                .IsRequired()
                .HasPrecision(6, 3);

            //TemperatureScale
            HasRequired(x => x.TemperatureScale)
                .WithMany(x => x.DSFamilyTempSensors)
                .HasForeignKey(x => x.TemperatureScaleId)
                .WillCascadeOnDelete(false);

            //TemperatureScaleId
            Property(x => x.TemperatureScaleId)
                .HasColumnOrder(7);

            //DSFamilyTempSensorResolution
            HasRequired(x => x.DSFamilyTempSensorResolution)
                .WithMany(x => x.DSFamilyTempSensors)
                .HasForeignKey(x => x.DSFamilyTempSensorResolutionId)
                .WillCascadeOnDelete(false);

            //DSFamilyTempSensorResolutionId
            Property(x => x.DSFamilyTempSensorResolutionId)
                .HasColumnOrder(8);
        }

        #endregion Constructors
    }
}