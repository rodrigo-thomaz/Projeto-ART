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

            //TempSensorRange
            HasRequired(x => x.TempSensorRange)
                .WithMany(x => x.DSFamilyTempSensors)
                .HasForeignKey(x => x.TempSensorRangeId)
                .WillCascadeOnDelete(false);

            //TempSensorRangeId
            Property(x => x.TempSensorRangeId)
                .HasColumnOrder(3);

            //DSFamilyTempSensorResolution
            HasRequired(x => x.DSFamilyTempSensorResolution)
                .WithMany(x => x.DSFamilyTempSensors)
                .HasForeignKey(x => x.DSFamilyTempSensorResolutionId)
                .WillCascadeOnDelete(false);

            //DSFamilyTempSensorResolutionId
            Property(x => x.DSFamilyTempSensorResolutionId)
                .HasColumnOrder(4);

            //LowChartLimiterCelsius
            Property(x => x.LowChartLimiterCelsius)
                .HasColumnOrder(5)
                .HasPrecision(7, 4)
                .HasColumnName("LowChartLimiterCelsius")
                .IsRequired();

            //HighChartLimiterCelsius
            Property(x => x.HighChartLimiterCelsius)
                .HasColumnOrder(6)
                .HasPrecision(7, 4)
                .HasColumnName("HighChartLimiterCelsius")
                .IsRequired();
        }

        #endregion Constructors
    }
}