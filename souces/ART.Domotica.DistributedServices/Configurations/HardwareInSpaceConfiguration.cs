using ART.Domotica.DistributedServices.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
{
    public class HardwareInSpaceConfiguration : EntityTypeConfiguration<HardwareInSpace>
    {
        public HardwareInSpaceConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.HardwareBaseId,
                x.SpaceId,
            });

            //HardwareBase
            HasRequired(x => x.HardwareBase)
                .WithMany(x => x.HardwaresInSpace)
                .HasForeignKey(x => x.HardwareBaseId)
                .WillCascadeOnDelete(false);

            //Space
            HasRequired(x => x.Space)
                .WithMany(x => x.HardwaresInSpace)
                .HasForeignKey(x => x.SpaceId)
                .WillCascadeOnDelete(false);
        }
    }
}