using ART.Data.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Data.Repository.Configurations
{
    public class ThermometerDeviceConfiguration : EntityTypeConfiguration<ThermometerDevice>
    {
        public ThermometerDeviceConfiguration()
        {
            ToTable("ThermometerDevice");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();

            //Id
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
        }
    }
}