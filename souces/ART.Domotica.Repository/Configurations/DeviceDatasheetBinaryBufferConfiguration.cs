namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceDatasheetBinaryBufferConfiguration : EntityTypeConfiguration<DeviceDatasheetBinaryBuffer>
    {
        #region Constructors

        public DeviceDatasheetBinaryBufferConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.Id,
                x.DeviceDatasheetId,
            });

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceBase
            HasRequired(x => x.DeviceDatasheetBinary)
               .WithRequiredDependent(x => x.DeviceDatasheetBinaryBuffer);

            //Binary
            Property(x => x.Buffer)
                .HasColumnOrder(2)
                .IsRequired();
        }

        #endregion Constructors
    }
}