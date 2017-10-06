namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class RaspberryDeviceBaseConfiguration : EntityTypeConfiguration<RaspberryDeviceBase>
    {
        #region Constructors

        public RaspberryDeviceBaseConfiguration()
        {
            ToTable("RaspberryDeviceBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .IsRequired();

            //LanMacAddress
            Property(x => x.LanMacAddress)
                .HasColumnOrder(1)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //WLanMacAddress
            Property(x => x.WLanMacAddress)
                .HasColumnOrder(2)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }

        #endregion Constructors
    }
}