using ART.Domotica.DistributedServices.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.DistributedServices.Configurations
{
    public class DSFamilyTempSensorResolutionConfiguration : EntityTypeConfiguration<DSFamilyTempSensorResolution>
    {
        public DSFamilyTempSensorResolutionConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //Name
            Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            //Resolution
            Property(x => x.Resolution)
                .HasPrecision(5,4);

            //ResolutionDecimalPlaces
            Ignore(x => x.ResolutionDecimalPlaces);
            
            //ConversionTime
            Property(x => x.ConversionTime)
                .HasPrecision(5, 2);
            
            //Description
            Property(x => x.Description)
                .HasMaxLength(5000)
                .IsOptional();
        }
    }
}