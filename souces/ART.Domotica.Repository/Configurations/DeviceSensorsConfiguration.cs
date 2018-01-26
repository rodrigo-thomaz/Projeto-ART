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
            HasKey(x => new
            {
                x.Id,
                x.DeviceDatasheetId,
            });

            // Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .IsRequired();

            //DeviceBase
            HasRequired(x => x.DeviceBase)
               .WithRequiredDependent(x => x.DeviceSensors);

            //ReadIntervalInMilliSeconds
            Property(x => x.ReadIntervalInMilliSeconds)
                .HasColumnOrder(2)
                .IsRequired();

            //PublishIntervalInMilliSeconds
            Property(x => x.PublishIntervalInMilliSeconds)
                .HasColumnOrder(3)
                .IsRequired();
        }

        #endregion Constructors
    }
}