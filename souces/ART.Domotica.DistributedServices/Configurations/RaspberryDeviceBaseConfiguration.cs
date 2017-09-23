using ART.Domotica.DistributedServices.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
{
    public class RaspberryDeviceBaseConfiguration : EntityTypeConfiguration<RaspberryDeviceBase>
    {
        public RaspberryDeviceBaseConfiguration()
        {
            ToTable("RaspberryDeviceBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();
        }
    }
}