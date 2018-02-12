namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceSensorConfiguration : EntityTypeConfiguration<DeviceSensor>
    {
        #region Constructors

        public DeviceSensorConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.DeviceTypeId,
                x.DeviceDatasheetId,
                x.Id,
            });

            //DeviceTypeId
            Property(x => x.DeviceTypeId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            // Id
            Property(x => x.Id)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceBase
            HasRequired(x => x.DeviceBase)
               .WithRequiredDependent(x => x.DeviceSensor);

            //ReadIntervalInMilliSeconds
            Property(x => x.ReadIntervalInMilliSeconds)
                .HasColumnOrder(3)
                .IsRequired();

            //PublishIntervalInMilliSeconds
            Property(x => x.PublishIntervalInMilliSeconds)
                .HasColumnOrder(4)
                .IsRequired();
        }

        #endregion Constructors
    }
}