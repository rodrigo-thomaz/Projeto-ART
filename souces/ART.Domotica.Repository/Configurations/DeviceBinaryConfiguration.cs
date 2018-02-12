namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceBinaryConfiguration : EntityTypeConfiguration<DeviceBinary>
    {
        #region Constructors

        public DeviceBinaryConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.DeviceTypeId,
                x.DeviceDatasheetId,
                x.Id,
            });

            //DeviceBase
            HasRequired(x => x.DeviceBase)
               .WithRequiredDependent(x => x.DeviceBinary);

            //DeviceTypeId
            Property(x => x.DeviceTypeId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .IsRequired();

            //DeviceDatasheetBinaryId
            Property(x => x.DeviceDatasheetBinaryId)
                .HasColumnOrder(2)
                .IsRequired();

            // Id
            Property(x => x.Id)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UpdateDate
            Property(x => x.UpdateDate)
                .HasColumnOrder(4)
                .IsRequired();

            //DeviceDatasheetBinary
            HasRequired(x => x.DeviceDatasheetBinary)
                .WithMany(x => x.DeviceBinaries)
                .HasForeignKey(x => new
                {
                    x.DeviceTypeId,
                    x.DeviceDatasheetId,
                    x.DeviceDatasheetBinaryId,
                })
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}