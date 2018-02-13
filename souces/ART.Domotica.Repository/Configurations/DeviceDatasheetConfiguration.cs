namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceDatasheetConfiguration : EntityTypeConfiguration<DeviceDatasheet>
    {
        #region Constructors

        public DeviceDatasheetConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.DeviceTypeId,
                x.Id,
            });

            //DeviceTypeId
            Property(x => x.DeviceTypeId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Id
            Property(x => x.Id)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceType
            HasRequired(x => x.DeviceType)
                .WithMany(x => x.DeviceDatasheets)
                .HasForeignKey(x => x.DeviceTypeId)
                .WillCascadeOnDelete(false);

            //Name
            Property(x => x.Name)
                .HasColumnOrder(2)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //HasDeviceBinary
            Property(x => x.HasDeviceBinary)
                .HasColumnOrder(3)
                .IsRequired();

            //HasDeviceDebug
            Property(x => x.HasDeviceDebug)
                .HasColumnOrder(4)
                .IsRequired();

            //HasDeviceDisplay
            Property(x => x.HasDeviceDisplay)
                .HasColumnOrder(5)
                .IsRequired();

            //HasDeviceMQ
            Property(x => x.HasDeviceMQ)
                .HasColumnOrder(6)
                .IsRequired();

            //HasDeviceNTP
            Property(x => x.HasDeviceNTP)
                .HasColumnOrder(7)
                .IsRequired();

            //HasDeviceSensor
            Property(x => x.HasDeviceSensor)
                .HasColumnOrder(8)
                .IsRequired();

            //HasDeviceSerial
            Property(x => x.HasDeviceSerial)
                .HasColumnOrder(9)
                .IsRequired();

            //HasDeviceWiFi
            Property(x => x.HasDeviceWiFi)
                .HasColumnOrder(10)
                .IsRequired();
        }

        #endregion Constructors
    }
}