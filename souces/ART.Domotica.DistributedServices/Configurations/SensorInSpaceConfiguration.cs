using ART.Domotica.DistributedServices.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
{
    public class SensorInSpaceConfiguration : EntityTypeConfiguration<SensorInSpace>
    {
        public SensorInSpaceConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.SensorId,
                x.SpaceId,
            });

            //SensorBase
            HasRequired(x => x.SensorBase)
                .WithMany(x => x.SensorInSpace)
                .HasForeignKey(x => x.SensorId)
                .WillCascadeOnDelete(false);

            //Space
            HasRequired(x => x.Space)
                .WithMany(x => x.SensorsInSpace)
                .HasForeignKey(x => x.SpaceId)
                .WillCascadeOnDelete(false);
        }
    }
}