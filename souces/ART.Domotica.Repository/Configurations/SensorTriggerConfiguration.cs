namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorTriggerConfiguration : EntityTypeConfiguration<SensorTrigger>
    {
        #region Constructors

        public SensorTriggerConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.Id,
                x.SensorId,
                x.SensorDatasheetId,
                x.SensorTypeId,
            });

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //SensorId
            Property(x => x.SensorId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorDatasheetId
            Property(x => x.SensorDatasheetId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorTypeId
            Property(x => x.SensorTypeId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Sensor
            HasRequired(x => x.Sensor)
                .WithMany(x => x.SensorTriggers)
                .HasForeignKey(x => new
                {
                    x.SensorId,
                    x.SensorDatasheetId,
                    x.SensorTypeId,
                })
                .WillCascadeOnDelete(false);

            //TriggerOn
            Property(x => x.TriggerOn)
                .HasColumnOrder(4)
                .IsRequired();

            //BuzzerOn
            Property(x => x.BuzzerOn)
                .HasColumnOrder(5)
                .IsRequired();

            //TriggerValue
            Property(x => x.TriggerValue)
                .HasColumnOrder(6)
                .HasMaxLength(50)
                .IsRequired();
        }

        #endregion Constructors
    }
}