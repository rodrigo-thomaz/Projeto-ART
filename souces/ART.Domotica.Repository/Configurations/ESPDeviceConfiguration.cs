namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class ESPDeviceConfiguration : EntityTypeConfiguration<ESPDevice>
    {
        #region Constructors

        public ESPDeviceConfiguration()
        {
            ToTable("ESPDevice");

            //Primary Keys
            HasKey(x => new
            {
                x.Id,
                x.DeviceDatasheetId,
            });

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .IsRequired();

            //ChipId
            Property(x => x.ChipId)
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]{
                        new IndexAttribute { IsUnique = false }, // ChipId não é único pois repete por lote http://bbs.espressif.com/viewtopic.php?t=1303
                    }));

            //FlashChipId
            Property(x => x.FlashChipId)
                .HasColumnOrder(3)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]{
                        new IndexAttribute { IsUnique = true }, // FlashId é único e imutável via código
                    }));            

            //SDKVersion
            Property(x => x.SDKVersion)
                .HasColumnOrder(4)
                .HasMaxLength(50)
                .IsRequired();

            //ChipSize
            Property(x => x.ChipSize)
                .HasColumnOrder(5)
                .IsRequired();

            //Pin
            Property(x => x.Pin)
                .HasColumnOrder(6)
                .HasMaxLength(4)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }

        #endregion Constructors
    }
}