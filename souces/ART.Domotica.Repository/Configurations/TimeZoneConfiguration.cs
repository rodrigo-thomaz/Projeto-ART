using ART.Domotica.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    public class TimeZoneConfiguration : EntityTypeConfiguration<TimeZone>
    {
        public TimeZoneConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //Zone
            HasRequired(x => x.Zone)
                .WithMany(x => x.TimeZones)
                .HasForeignKey(x => x.ZoneId)
                .WillCascadeOnDelete(false);

            //ZoneId
            Property(x => x.ZoneId)
                .HasColumnOrder(1);

            //Abreviation
            Property(x => x.Abreviation)
                .HasColumnOrder(2)
                .HasMaxLength(6)
                .IsRequired();

            //TimeStart
            Property(x => x.TimeStart)
                .HasColumnOrder(3)
                .HasPrecision(11,0)
                .IsRequired();

            //GMTOffset
            Property(x => x.GMTOffset)
                .HasColumnOrder(4)                
                .IsRequired();

            //DST
            Property(x => x.DST)
                .HasColumnOrder(5)
                .HasMaxLength(1)
                .IsFixedLength()
                .IsRequired();
        }
    }
}
