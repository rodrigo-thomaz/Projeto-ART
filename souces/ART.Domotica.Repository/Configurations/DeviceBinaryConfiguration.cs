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
                x.Id,
                x.DeviceDatasheetId,
            });

            //DeviceBase
            HasRequired(x => x.DeviceBase)
               .WithRequiredDependent(x => x.DeviceBinary);

            // Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceDatasheetBinaryId
            Property(x => x.DeviceDatasheetBinaryId)
                .HasColumnOrder(1)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(2)
                .IsRequired();

            //UpdateDate
            Property(x => x.UpdateDate)
                .HasColumnOrder(3)
                .IsRequired();

            //DeviceDatasheetBinary
            HasRequired(x => x.DeviceDatasheetBinary)
                .WithMany(x => x.DeviceBinaries)
                .HasForeignKey(x => new
                {
                    x.DeviceDatasheetBinaryId,
                    x.DeviceDatasheetId,
                })
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}