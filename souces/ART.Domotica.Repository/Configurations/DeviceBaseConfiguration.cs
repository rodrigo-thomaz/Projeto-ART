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
                x.Id,
                x.DeviceDatasheetId,
            });

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceDatasheet
            HasRequired(x => x.DeviceDatasheet)
                .WithMany(x => x.DevicesBase)
                .HasForeignKey(x => x.DeviceDatasheetId)
                .WillCascadeOnDelete(false);

            //Label
            Property(x => x.Label)
                .HasColumnOrder(2)
                .HasMaxLength(50)
                .IsRequired();

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(3)
                .IsRequired();
        }

        #endregion Constructors
    }
}