namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorConfiguration : EntityTypeConfiguration<Sensor>
    {
        #region Constructors

        public SensorConfiguration()
        {
            ToTable("Sensor");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnOrder(0)
                .IsRequired();

            //Label
            Property(x => x.Label)
                .HasColumnOrder(1)
                .HasMaxLength(50)
                .IsRequired();

            //UnitMeasurement
            HasRequired(x => x.UnitMeasurement)
                .WithMany(x => x.Sensors)
                .HasForeignKey(x => new
                {
                    x.UnitMeasurementId,
                    x.UnitMeasurementTypeId,
                })
                .WillCascadeOnDelete(false);

            //UnitMeasurementId
            Property(x => x.UnitMeasurementId)
                .HasColumnOrder(2);

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(3);

            //SensorRange
            HasOptional(x => x.SensorRange)
                .WithMany(x => x.Sensors)
                .HasForeignKey(x => x.SensorRangeId)
                .WillCascadeOnDelete(false);

            //SensorRangeId
            Property(x => x.SensorRangeId)
                .HasColumnOrder(4);

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(5)
                .IsRequired();
        }

        #endregion Constructors
    }
}