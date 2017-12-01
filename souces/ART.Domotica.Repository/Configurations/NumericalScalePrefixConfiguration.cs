namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class NumericalScalePrefixConfiguration : EntityTypeConfiguration<NumericalScalePrefix>
    {
        #region Constructors

        public NumericalScalePrefixConfiguration()
        {
            ToTable("NumericalScalePrefix", "SI");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Name
            Property(x => x.Name)
                .HasColumnOrder(1)
                .HasMaxLength(5)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Symbol
            Property(x => x.Symbol)
                .HasColumnOrder(2)
                .HasMaxLength(2)
                .IsOptional();
        }

        #endregion Constructors
    }
}