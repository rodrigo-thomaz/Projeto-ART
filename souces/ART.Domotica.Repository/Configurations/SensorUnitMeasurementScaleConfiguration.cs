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
                x.SensorDatasheetId,
                x.SensorTypeId,
                x.UnitMeasurementId,
                x.UnitMeasurementTypeId,
                x.NumericalScalePrefixId,
                x.NumericalScaleTypeId,
            });

            //SensorDatasheetId
            Property(x => x.SensorDatasheetId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorDatasheet
            HasRequired(x => x.SensorDatasheet)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.SensorDatasheetId,
                    x.SensorTypeId,
                })
                .WillCascadeOnDelete(false);

            //SensorTypeId
            Property(x => x.SensorTypeId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorType
            HasRequired(x => x.SensorType)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => x.SensorTypeId)
                .WillCascadeOnDelete(false);

            //UnitMeasurementId
            Property(x => x.UnitMeasurementId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurement
            HasRequired(x => x.UnitMeasurement)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.UnitMeasurementId,
                    x.UnitMeasurementTypeId,
                })
                .WillCascadeOnDelete(false);

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementType
            HasRequired(x => x.UnitMeasurementType)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => x.UnitMeasurementTypeId)
                .WillCascadeOnDelete(false);

            //NumericalScalePrefixId
            Property(x => x.NumericalScalePrefixId)
                .HasColumnOrder(4)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScalePrefix
            HasRequired(x => x.NumericalScalePrefix)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => x.NumericalScalePrefixId)
                .WillCascadeOnDelete(false);

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(5)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScaleType
            HasRequired(x => x.NumericalScaleType)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => x.NumericalScaleTypeId)
                .WillCascadeOnDelete(false);

            //UnitMeasurementScale
            HasRequired(x => x.UnitMeasurementScale)
                .WithMany(x => x.SensorUnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.UnitMeasurementId,
                    x.UnitMeasurementTypeId,
                    x.NumericalScalePrefixId,
                    x.NumericalScaleTypeId,
                })
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}