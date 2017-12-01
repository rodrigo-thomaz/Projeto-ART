namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorUnitMeasurementDefaultConfiguration : EntityTypeConfiguration<SensorUnitMeasurementDefault>
    {
        #region Constructors

        public SensorUnitMeasurementDefaultConfiguration()
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

            //SensorType
            HasRequired(x => x.SensorType)
                .WithMany(x => x.SensorUnitMeasurementDefaults)
                .HasForeignKey(x => x.SensorTypeId)
                .WillCascadeOnDelete(false);

            //SensorDatasheet
            HasRequired(x => x.SensorDatasheet)
               .WithRequiredDependent(x => x.SensorUnitMeasurementDefault);

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

            //UnitMeasurement
            HasRequired(x => x.UnitMeasurement)
                .WithMany(x => x.SensorUnitMeasurementDefaults)
                .HasForeignKey(x => new
                {
                    x.UnitMeasurementId,
                    x.UnitMeasurementTypeId,
                })
                .WillCascadeOnDelete(false);

            //Min
            Property(x => x.Min)
                .HasColumnOrder(4)
                .HasPrecision(9,4)
                .IsRequired();

            //Max
            Property(x => x.Max)
                .HasColumnOrder(5)
                .HasPrecision(9, 4)
                .IsRequired();
        }

        #endregion Constructors
    }
}