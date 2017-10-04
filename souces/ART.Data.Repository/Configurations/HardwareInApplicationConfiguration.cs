using ART.Data.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Data.Repository.Configurations
{
    public class HardwareInApplicationConfiguration : EntityTypeConfiguration<HardwareInApplication>
    {
        public HardwareInApplicationConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.HardwareBaseId,
                x.ApplicationId,
            });

            //HardwareBaseId
            Property(x => x.HardwareBaseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //ApplicationId
            Property(x => x.ApplicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //HardwareBase
            HasRequired(x => x.HardwareBase)
                .WithMany(x => x.HardwaresInApplication)
                .HasForeignKey(x => x.HardwareBaseId)
                .WillCascadeOnDelete(false);

            //Application
            HasRequired(x => x.Application)
                .WithMany(x => x.HardwaresInApplication)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);
        }
    }
}