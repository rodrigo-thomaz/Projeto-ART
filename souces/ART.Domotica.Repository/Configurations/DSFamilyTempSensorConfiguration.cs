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

            //TemperatureScale
            HasRequired(x => x.TemperatureScale)
                .WithMany(x => x.DSFamilyTempSensors)
                .HasForeignKey(x => x.TemperatureScaleId)
                .WillCascadeOnDelete(false);

            //TemperatureScaleId
            Property(x => x.TemperatureScaleId)
                .HasColumnOrder(3);

            //DSFamilyTempSensorResolution
            HasRequired(x => x.DSFamilyTempSensorResolution)
                .WithMany(x => x.DSFamilyTempSensors)
                .HasForeignKey(x => x.DSFamilyTempSensorResolutionId)
                .WillCascadeOnDelete(false);

            //DSFamilyTempSensorResolutionId
            Property(x => x.DSFamilyTempSensorResolutionId)
                .HasColumnOrder(4);

            //LowTempSensorAlarm.AlarmOn
            Property(x => x.LowAlarm.AlarmOn)
                .HasColumnOrder(5)
                .HasColumnName("LowAlarmOn")
                .IsRequired();

            //LowTempSensorAlarm.AlarmCelsius
            Property(x => x.LowAlarm.AlarmCelsius)
                .HasColumnOrder(6)
                .HasPrecision(7, 4)
                .HasColumnName("LowAlarmCelsius")
                .IsRequired();

            //LowTempSensorAlarm.AlarmBuzzerOn
            Property(x => x.LowAlarm.AlarmBuzzerOn)
                .HasColumnOrder(7)
                .HasColumnName("LowAlarmBuzzerOn")
                .IsRequired();

            //HighTempSensorAlarm.AlarmOn
            Property(x => x.HighAlarm.AlarmOn)
                .HasColumnOrder(8)
                .HasColumnName("HighAlarmOn")
                .IsRequired();

            //HighTempSensorAlarm.AlarmCelsius
            Property(x => x.HighAlarm.AlarmCelsius)
                .HasColumnOrder(9)
                .HasPrecision(7, 4)
                .HasColumnName("HighAlarmCelsius")
                .IsRequired();

            //HighTempSensorAlarm.AlarmBuzzerOn
            Property(x => x.HighAlarm.AlarmBuzzerOn)
                .HasColumnOrder(10)
                .HasColumnName("HighAlarmBuzzerOn")
                .IsRequired();
        }

        #endregion Constructors
    }
}