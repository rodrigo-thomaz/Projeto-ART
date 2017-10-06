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

            //MacAddress
            Property(x => x.MacAddress)
                .HasColumnOrder(1)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }

        #endregion Constructors
    }
}