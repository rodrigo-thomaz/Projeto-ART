namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceSensorsConfiguration : EntityTypeConfiguration<DeviceSensors>
    {
        #region Constructors

        public DeviceSensorsConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            // Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceBase
            HasRequired(x => x.DeviceBase)
               .WithRequiredDependent(x => x.DeviceSensors);

            //PublishIntervalInSeconds
            Property(x => x.PublishIntervalInSeconds)
                .HasColumnOrder(1)
                .IsRequired();            
        }

        #endregion Constructors
    }
}