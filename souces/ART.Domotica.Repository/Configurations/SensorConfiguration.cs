namespace ART.Domotica.Repository.Configurations
{
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
                .HasColumnOrder(0)
                .IsRequired();

            //SensorDatasheetId
            Property(x => x.Id)
                .HasColumnOrder(1)
                .IsRequired();

            //SensorDatasheet
            HasRequired(x => x.SensorDatasheet)
                .WithMany(x => x.Sensors)
                .HasForeignKey(x => new
                {
                    x.SensorDatasheetId,
                    x.SensorTypeId,
                })
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}