using ART.Domotica.DistributedServices.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
{
    public class ESPDeviceBaseConfiguration : EntityTypeConfiguration<ESPDeviceBase>
    {
        public ESPDeviceBaseConfiguration()
        {
            ToTable("ESPDeviceBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();
        }
    }
}