namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceBaseConfiguration : EntityTypeConfiguration<DeviceBase>
    {
        #region Constructors

        public DeviceBaseConfiguration()
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
                .WithMany(x => x.DevicesBase)
                .HasForeignKey(x => new
                {
                    x.DeviceTypeId,
                    x.DeviceDatasheetId,
                })
                .WillCascadeOnDelete(false);

            //Label
            Property(x => x.Label)
                .HasColumnOrder(3)
                .HasMaxLength(50)
                .IsRequired();

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(4)
                .IsRequired();
        }

        #endregion Constructors
    }
}