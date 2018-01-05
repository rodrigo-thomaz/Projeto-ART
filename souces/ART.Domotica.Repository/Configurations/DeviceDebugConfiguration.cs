namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceDebugConfiguration : EntityTypeConfiguration<DeviceDebug>
    {
        #region Constructors

        public DeviceDebugConfiguration()
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
                .IsRequired();

            //DeviceBase
            HasRequired(x => x.DeviceBase)
               .WithRequiredDependent(x => x.DeviceDebug);

            //RemoteEnabled
            Property(x => x.RemoteEnabled)
                .HasColumnOrder(2)
                .IsRequired();

            //SerialEnabled
            Property(x => x.SerialEnabled)
                .HasColumnOrder(3)
                .IsRequired();

            //ResetCmdEnabled
            Property(x => x.ResetCmdEnabled)
                .HasColumnOrder(4)
                .IsRequired();

            //ShowDebugLevel
            Property(x => x.ShowDebugLevel)
                .HasColumnOrder(5)
                .IsRequired();

            //ShowTime
            Property(x => x.ShowTime)
                .HasColumnOrder(6)
                .IsRequired();

            //ShowProfiler
            Property(x => x.ShowProfiler)
                .HasColumnOrder(7)
                .IsRequired();

            //ShowColors
            Property(x => x.ShowColors)
                .HasColumnOrder(8)
                .IsRequired();
        }

        #endregion Constructors
    }
}