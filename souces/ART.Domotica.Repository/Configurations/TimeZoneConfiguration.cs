namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class TimeZoneConfiguration : EntityTypeConfiguration<TimeZone>
    {
        #region Constructors

        public TimeZoneConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //DisplayName
            Property(x => x.DisplayName)
                .HasColumnOrder(1)
                .HasMaxLength(100)
                .IsRequired();

            //SupportsDaylightSavingTime
            Property(x => x.SupportsDaylightSavingTime)
                .HasColumnOrder(2)
                .IsRequired();

            //UtcTimeOffsetInSecond
            Property(x => x.UtcTimeOffsetInSecond)
                .HasColumnOrder(3)
                .IsRequired();
        }

        #endregion Constructors
    }
}