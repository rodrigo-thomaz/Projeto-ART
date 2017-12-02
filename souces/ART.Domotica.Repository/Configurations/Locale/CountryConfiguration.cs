namespace ART.Domotica.Repository.Configurations.Locale
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities.Locale;

    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        #region Constructors

        public CountryConfiguration()
        {
            ToTable("Country", "Locale");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //Name
            Property(x => x.Name)
                .HasColumnOrder(1)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //ContinentId
            Property(x => x.ContinentId)
                .HasColumnOrder(2)
                .IsRequired();

            //Continent
            HasRequired(x => x.Continent)
                .WithMany(x => x.Countries)
                .HasForeignKey(x => x.ContinentId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}