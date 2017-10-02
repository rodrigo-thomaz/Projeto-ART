using ART.Data.Repository.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Data.Repository.Configurations
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