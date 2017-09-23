using ART.Domotica.DistributedServices.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
{
    public class DSFamilyTempSensorConfiguration : EntityTypeConfiguration<DSFamilyTempSensor>
    {
        public DSFamilyTempSensorConfiguration()
        {
            ToTable("DSFamilyTempSensor");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();
        }
    }
}