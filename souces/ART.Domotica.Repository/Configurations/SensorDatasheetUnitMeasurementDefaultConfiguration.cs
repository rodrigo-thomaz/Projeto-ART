namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorDatasheetUnitMeasurementDefaultConfiguration : EntityTypeConfiguration<SensorDatasheetUnitMeasurementDefault>
    {
        #region Constructors

        public SensorDatasheetUnitMeasurementDefaultConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.Id,
                x.SensorTypeId,
            });

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorTypeId
            Property(x => x.SensorTypeId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorDatasheet
            HasRequired(x => x.SensorDatasheet)
               .WithRequiredDependent(x => x.SensorDatasheetUnitMeasurementDefault);

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementId
            Property(x => x.UnitMeasurementId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(4)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScalePrefixId
            Property(x => x.NumericalScalePrefixId)
                .HasColumnOrder(5)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();            

            //UnitMeasurementScale
            HasRequired(x => x.UnitMeasurementScale)
                .WithMany(x => x.SensorDatasheetUnitMeasurementDefaults)
                .HasForeignKey(x => new
                {
                    x.UnitMeasurementTypeId,
                    x.UnitMeasurementId,
                    x.NumericalScaleTypeId,
                    x.NumericalScalePrefixId,                    
                })
                .WillCascadeOnDelete(false);

            //Min
            Property(x => x.Min)
                .HasColumnOrder(6)
                .HasPrecision(9,4)
                .IsRequired();

            //Max
            Property(x => x.Max)
                .HasColumnOrder(7)
                .HasPrecision(9, 4)
                .IsRequired();
        }

        #endregion Constructors
    }
}