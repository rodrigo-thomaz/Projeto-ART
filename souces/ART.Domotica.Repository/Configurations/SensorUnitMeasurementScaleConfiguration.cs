namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorUnitMeasurementScaleConfiguration : EntityTypeConfiguration<SensorUnitMeasurementScale>
    {
        #region Constructors

        public SensorUnitMeasurementScaleConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.Id,
                x.SensorDatasheetId,
                x.SensorTypeId,
            });

            // Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorDatasheetId
            Property(x => x.SensorDatasheetId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorTypeId
            Property(x => x.SensorTypeId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Sensor
            HasRequired(x => x.Sensor)
               .WithRequiredDependent(x => x.SensorUnitMeasurementScale);

            //UnitMeasurementId
            Property(x => x.UnitMeasurementId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(4)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScalePrefixId
            Property(x => x.NumericalScalePrefixId)
                .HasColumnOrder(5)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(6)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementScale
            HasRequired(x => x.SensorDatasheetUnitMeasurementScale)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.SensorDatasheetId,
                    x.SensorTypeId,
                    x.UnitMeasurementId,
                    x.UnitMeasurementTypeId,
                    x.NumericalScalePrefixId,
                    x.NumericalScaleTypeId,
                })
                .WillCascadeOnDelete(false);

            //CountryId
            Property(x => x.CountryId)
                .HasColumnOrder(7)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementScale
            HasRequired(x => x.NumericalScaleTypeCountry)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.NumericalScaleTypeId,
                    x.CountryId,
                })
                .WillCascadeOnDelete(false);

            //RangeMax
            Property(x => x.RangeMax)
                .HasColumnOrder(8)
                .HasPrecision(7, 4)
                .IsRequired();

            //RangeMin
            Property(x => x.RangeMin)
                .HasColumnOrder(9)
                .HasPrecision(7, 4)
                .IsRequired();

            //ChartLimiterMax
            Property(x => x.ChartLimiterMax)
                .HasColumnOrder(10)
                .HasPrecision(7, 4)
                .IsRequired();

            //ChartLimiterMin
            Property(x => x.ChartLimiterMin)
                .HasColumnOrder(11)
                .HasPrecision(7, 4)
                .IsRequired();            
        }

        #endregion Constructors
    }
}