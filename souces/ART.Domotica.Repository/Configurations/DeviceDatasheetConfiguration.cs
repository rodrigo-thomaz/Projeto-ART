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

            //HasDeviceSerial
            Property(x => x.HasDeviceSerial)
                .HasColumnOrder(3)
                .IsRequired();

            //HasDeviceSensors
            Property(x => x.HasDeviceSensors)
                .HasColumnOrder(4)
                .IsRequired();
        }

        #endregion Constructors
    }
}