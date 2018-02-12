namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class RaspberryDeviceConfiguration : EntityTypeConfiguration<RaspberryDevice>
    {
        #region Constructors

        public RaspberryDeviceConfiguration()
        {
            ToTable("RaspberryDevice");

            //Primary Keys
            HasKey(x => new
            {
                x.DeviceTypeId,
                x.DeviceDatasheetId,
                x.Id,
            });

            // DeviceTypeId
            Property(x => x.DeviceTypeId)
                .HasColumnOrder(0)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .IsRequired();

            //Id
            Property(x => x.Id)
                .HasColumnOrder(2)
                .IsRequired();

            //LanMacAddress
            Property(x => x.LanMacAddress)
                .HasColumnOrder(3)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //WLanMacAddress
            Property(x => x.WLanMacAddress)
                .HasColumnOrder(4)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }

        #endregion Constructors
    }
}