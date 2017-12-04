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
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //SensorId
            Property(x => x.SensorId)
                .HasColumnOrder(1)
                .IsRequired();

            //Sensor
            HasRequired(x => x.Sensor)
                .WithMany(x => x.SensorTriggers)
                .HasForeignKey(x => x.SensorId)
                .WillCascadeOnDelete(false);

            //TriggerOn
            Property(x => x.TriggerOn)
                .HasColumnOrder(2)
                .IsRequired();            

            //BuzzerOn
            Property(x => x.BuzzerOn)
                .HasColumnOrder(3)
                .IsRequired();

            //TriggerValue
            Property(x => x.TriggerValue)
                .HasColumnOrder(4)
                .HasMaxLength(50)
                .IsRequired();
        }

        #endregion Constructors
    }
}