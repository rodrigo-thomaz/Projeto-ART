namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class UnitMeasurementScaleConfiguration : EntityTypeConfiguration<UnitMeasurementScale>
    {
        #region Constructors

        public UnitMeasurementScaleConfiguration()
        {
            ToTable("UnitMeasurementScale", "SI");

            //Primary Keys
            HasKey(x => new
            {
                x.UnitMeasurementPrefixId,
                x.NumericalScaleId,
            });

            //UnitMeasurementPrefixId
            Property(x => x.UnitMeasurementPrefixId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementPrefix
            HasRequired(x => x.UnitMeasurementPrefix)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => x.UnitMeasurementPrefixId)
                .WillCascadeOnDelete(false);

            //NumericalScaleId
            Property(x => x.NumericalScaleId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScale
            HasRequired(x => x.NumericalScale)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => x.NumericalScaleId)
                .WillCascadeOnDelete(false);

            //Name
            Property(x => x.Name)
                .HasColumnOrder(2)
                .HasMaxLength(5)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Base
            Property(x => x.Base)
                .HasColumnOrder(3)
                .IsRequired();

            //Exponent
            Property(x => x.Exponent)
                .HasColumnOrder(4)
                .IsRequired();
        }

        #endregion Constructors
    }
}