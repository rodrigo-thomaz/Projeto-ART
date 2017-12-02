namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities.SI;

    public class UnitMeasurementConfiguration : EntityTypeConfiguration<UnitMeasurement>
    {
        #region Constructors

        public UnitMeasurementConfiguration()
        {
            ToTable("UnitMeasurement", "SI");

            //Primary Keys
            HasKey(x => new
            {
                x.Id,
                x.UnitMeasurementTypeId,
            });

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementType
            HasRequired(x => x.UnitMeasurementType)
                .WithMany(x => x.UnitMeasurements)
                .HasForeignKey(x => x.UnitMeasurementTypeId)
                .WillCascadeOnDelete(false);

            //Name
            Property(x => x.Name)
                .HasColumnOrder(2)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Symbol
            Property(x => x.Symbol)
                .HasColumnOrder(3)
                .HasMaxLength(2)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Description
            Property(x => x.Description)
                .HasColumnOrder(4)
                .HasMaxLength(5000)
                .IsOptional();
        }

        #endregion Constructors
    }
}