using ART.MQ.Worker.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.MQ.Worker.Configurations
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

            //HardwareBaseId
            Property(x => x.HardwareBaseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SpaceId
            Property(x => x.SpaceId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

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