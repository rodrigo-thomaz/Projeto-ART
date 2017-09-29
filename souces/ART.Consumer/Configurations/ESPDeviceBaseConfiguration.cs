using ART.Consumer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ART.Consumer.Configurations
{
    public class ESPDeviceBaseConfiguration : EntityTypeConfiguration<ESPDeviceBase>
    {
        public ESPDeviceBaseConfiguration()
        {
            ToTable("ESPDeviceBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();

            //MacAddress
            Property(x => x.MacAddress)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}