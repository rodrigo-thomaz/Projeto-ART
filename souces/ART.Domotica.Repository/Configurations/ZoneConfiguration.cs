using ART.Domotica.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    public class ZoneConfiguration : EntityTypeConfiguration<Zone>
    {
        public ZoneConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Country
            HasRequired(x => x.Country)
                .WithMany(x => x.Zones)
                .HasForeignKey(x => x.CountryId)
                .WillCascadeOnDelete(false);

            //CountryId
            Property(x => x.CountryId)
                .HasColumnOrder(1);

            //Name
            Property(x => x.Name)
                .HasColumnOrder(2)
                .HasMaxLength(35)
                .IsRequired();
        }
    }
}
