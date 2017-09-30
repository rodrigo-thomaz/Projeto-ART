using ART.MQ.Consumer.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.MQ.Consumer.Configurations
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