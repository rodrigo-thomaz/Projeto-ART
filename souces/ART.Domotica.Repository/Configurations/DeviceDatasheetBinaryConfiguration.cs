namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceDatasheetBinaryConfiguration : EntityTypeConfiguration<DeviceDatasheetBinary>
    {
        #region Constructors

        public DeviceDatasheetBinaryConfiguration()
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

            //Id
            Property(x => x.Id)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //DeviceDatasheet
            HasRequired(x => x.DeviceDatasheet)
                .WithMany(x => x.DeviceDatasheetBinaries)
                .HasForeignKey(x => new
                {
                    x.DeviceTypeId,
                    x.DeviceDatasheetId,
                })
                .WillCascadeOnDelete(false);

            //Version
            Property(x => x.Version)
                .HasColumnOrder(3)
                .IsRequired();

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(4)
                .IsRequired();
        }

        #endregion Constructors
    }
}