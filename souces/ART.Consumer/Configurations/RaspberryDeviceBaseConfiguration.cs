using ART.Consumer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ART.Consumer.Configurations
{
    public class RaspberryDeviceBaseConfiguration : EntityTypeConfiguration<RaspberryDeviceBase>
    {
        public RaspberryDeviceBaseConfiguration()
        {
            ToTable("RaspberryDeviceBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();

            //LanMacAddress
            Property(x => x.LanMacAddress)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //WLanMacAddress
            Property(x => x.WLanMacAddress)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}