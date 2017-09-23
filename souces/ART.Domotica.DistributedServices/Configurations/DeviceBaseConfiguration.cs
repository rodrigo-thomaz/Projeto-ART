using ART.Domotica.DistributedServices.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
{
    public class DeviceBaseConfiguration : EntityTypeConfiguration<DeviceBase>
    {
        public DeviceBaseConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
        }
    }
}