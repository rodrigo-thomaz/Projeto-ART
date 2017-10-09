namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;
    using System.Data.Entity.Infrastructure.Annotations;

    public class HardwareBaseConfiguration : EntityTypeConfiguration<HardwareBase>
    {
        #region Constructors

        public HardwareBaseConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //Pin
            Property(x => x.Pin)
                .HasColumnOrder(1)
                .HasMaxLength(4)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(2)
                .IsRequired();
        }

        #endregion Constructors
    }
}