namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorDatasheetUnitMeasurementScaleConfiguration : EntityTypeConfiguration<SensorDatasheetUnitMeasurementScale>
    {
        #region Constructors

        public SensorDatasheetUnitMeasurementScaleConfiguration()
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

            //SensorTypeId
            Property(x => x.SensorTypeId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementId
            Property(x => x.UnitMeasurementId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScalePrefixId
            Property(x => x.NumericalScalePrefixId)
                .HasColumnOrder(4)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(5)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorDatasheet
            HasRequired(x => x.SensorDatasheet)
                .WithMany(x => x.SensorDatasheetUnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.SensorDatasheetId,
                    x.SensorTypeId,
                })
                .WillCascadeOnDelete(false);

            //UnitMeasurementScale
            HasRequired(x => x.UnitMeasurementScale)
                .WithMany(x => x.SensorDatasheetUnitMeasurementScales)
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