namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorUnitOfMeasurementDefaultConfiguration : EntityTypeConfiguration<SensorUnitOfMeasurementDefault>
    {
        #region Constructors

        public SensorUnitOfMeasurementDefaultConfiguration()
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
                .WithMany(x => x.SensorUnitOfMeasurementDefaults)
                .HasForeignKey(x => x.SensorTypeId)
                .WillCascadeOnDelete(false);

            //SensorDatasheet
            HasRequired(x => x.SensorDatasheet)
               .WithRequiredDependent(x => x.SensorUnitOfMeasurementDefault);

            //UnitOfMeasurementId
            Property(x => x.UnitOfMeasurementId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitOfMeasurementTypeId
            Property(x => x.UnitOfMeasurementTypeId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitOfMeasurement
            HasRequired(x => x.UnitOfMeasurement)
                .WithMany(x => x.SensorUnitOfMeasurementDefaults)
                .HasForeignKey(x => new
                {
                    x.UnitOfMeasurementId,
                    x.UnitOfMeasurementTypeId,
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