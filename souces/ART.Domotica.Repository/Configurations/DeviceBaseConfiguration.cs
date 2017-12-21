namespace ART.Domotica.Repository.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DeviceBaseConfiguration : EntityTypeConfiguration<DeviceBase>
    {
        #region Constructors

        public DeviceBaseConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //Label
            Property(x => x.Label)
                .HasColumnOrder(1)
                .HasMaxLength(50)
                .IsRequired();

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(2)
                .IsRequired();
        }

        #endregion Constructors
    }
}