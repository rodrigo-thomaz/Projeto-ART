using ART.Domotica.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //Code
            Property(x => x.Code)
                .HasColumnOrder(1)
                .HasMaxLength(2)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]{
                        new IndexAttribute { IsUnique = true },
                    })); ;

            //Name
            Property(x => x.Name)
                .HasColumnOrder(2)
                .HasMaxLength(45)
                .IsRequired();
        }
    }
}
