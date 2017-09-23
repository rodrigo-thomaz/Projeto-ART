using ART.Domotica.DistributedServices.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
{
    public class DeviceBaseConfiguration : EntityTypeConfiguration<DeviceBase>
    {
        public DeviceBaseConfiguration()
        {
            ToTable("DeviceBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();
        }
    }
}