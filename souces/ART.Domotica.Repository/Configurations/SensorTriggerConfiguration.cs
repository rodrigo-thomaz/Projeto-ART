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

            //SensorBaseId
            Property(x => x.SensorBaseId)
                .HasColumnOrder(1)
                .IsRequired();

            //SensorBase
            HasRequired(x => x.SensorBase)
                .WithMany(x => x.SensorTriggers)
                .HasForeignKey(x => x.SensorBaseId)
                .WillCascadeOnDelete(false);

            //TriggerOn
            Property(x => x.TriggerOn)
                .HasColumnOrder(2)
                .IsRequired();

            //TriggerValue
            Property(x => x.TriggerValue)
                .HasColumnOrder(3)
                .HasMaxLength(50)
                .IsRequired();

            //BuzzerOn
            Property(x => x.BuzzerOn)
                .HasColumnOrder(4)
                .IsRequired();
        }

        #endregion Constructors
    }
}