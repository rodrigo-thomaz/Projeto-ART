namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class ESPDeviceBaseConfiguration : EntityTypeConfiguration<ESPDeviceBase>
    {
        #region Constructors

        public ESPDeviceBaseConfiguration()
        {
            ToTable("ESPDeviceBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .IsRequired();

            //ChipId
            Property(x => x.ChipId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new []{
                        new IndexAttribute { IsUnique = false }, // ChipId não é único pois repete por lote http://bbs.espressif.com/viewtopic.php?t=1303
                    }));

            //FlashChipId
            Property(x => x.FlashChipId)
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]{
                        new IndexAttribute { IsUnique = true }, // FlashId é único e imutável via código
                    }));

            //MacAddress
            Property(x => x.MacAddress)
                .HasColumnOrder(3)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]{
                        new IndexAttribute { IsUnique = true }, // MacAddress é único mas mutável via código
                    }));

            //Pin
            Property(x => x.Pin)
                .HasColumnOrder(4)
                .HasMaxLength(4)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //TimeOffset
            Property(x => x.TimeOffset)
                .HasColumnOrder(5)
                .IsRequired();
        }

        #endregion Constructors
    }
}