using ART.Consumer.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Consumer.Configurations
{
    public class SensorBaseConfiguration : EntityTypeConfiguration<SensorBase>
    {
        public SensorBaseConfiguration()
        {
            ToTable("SensorBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();
        }
    }
}