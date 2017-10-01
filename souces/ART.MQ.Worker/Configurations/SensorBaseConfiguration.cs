using ART.MQ.Worker.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.MQ.Worker.Configurations
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