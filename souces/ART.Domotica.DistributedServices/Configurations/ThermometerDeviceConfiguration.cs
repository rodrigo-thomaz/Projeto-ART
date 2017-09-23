using ART.Domotica.DistributedServices.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
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
        }
    }
}