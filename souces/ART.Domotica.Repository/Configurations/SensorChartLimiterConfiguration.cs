namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorChartLimiterConfiguration : EntityTypeConfiguration<SensorChartLimiter>
    {
        #region Constructors

        public SensorChartLimiterConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Sensor
            HasRequired(x => x.Sensor)
               .WithRequiredDependent(x => x.SensorChartLimiter);

            //UnitMeasurementId
            Property(x => x.UnitMeasurementId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScalePrefixId
            Property(x => x.NumericalScalePrefixId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(4)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementScale
            HasRequired(x => x.UnitMeasurementScale)
                .WithMany(x => x.SensorChartLimiters)
                .HasForeignKey(x => new
                {
                    x.UnitMeasurementId,
                    x.UnitMeasurementTypeId,
                    x.NumericalScalePrefixId,
                    x.NumericalScaleTypeId,
                })
                .WillCascadeOnDelete(false);

            //RangeMax
            Property(x => x.RangeMax)
                .HasColumnOrder(5)
                .HasPrecision(7, 4)
                .IsRequired();

            //RangeMin
            Property(x => x.RangeMin)
                .HasColumnOrder(6)
                .HasPrecision(7, 4)
                .IsRequired();

            //ChartLimiterMax
            Property(x => x.ChartLimiterMax)
                .HasColumnOrder(7)
                .HasPrecision(7, 4)
                .IsRequired();

            //ChartLimiterMin
            Property(x => x.ChartLimiterMin)
                .HasColumnOrder(8)
                .HasPrecision(7, 4)
                .IsRequired();


        }

        #endregion Constructors
    }
}