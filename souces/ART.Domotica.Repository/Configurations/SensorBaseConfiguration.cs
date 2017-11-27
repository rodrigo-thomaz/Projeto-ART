namespace ART.Domotica.Repository.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorBaseConfiguration : EntityTypeConfiguration<SensorBase>
    {
        #region Constructors

        public SensorBaseConfiguration()
        {
            ToTable("SensorBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .IsRequired();

            //UnitOfMeasurement
            HasRequired(x => x.UnitOfMeasurement)
                .WithMany(x => x.Sensors)
                .HasForeignKey(x => x.UnitOfMeasurementId)
                .WillCascadeOnDelete(false);

            //UnitOfMeasurementId
            Property(x => x.UnitOfMeasurementId)
                .HasColumnOrder(1);
        }

        #endregion Constructors
    }
}