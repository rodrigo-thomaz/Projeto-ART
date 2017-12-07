namespace ART.Domotica.Repository.Configurations.SI
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities.SI;

    public class UnitMeasurementScaleConfiguration : EntityTypeConfiguration<UnitMeasurementScale>
    {
        #region Constructors

        public UnitMeasurementScaleConfiguration()
        {
            ToTable("UnitMeasurementScale", "SI");

            //Primary Keys
            HasKey(x => new
            {
                x.UnitMeasurementTypeId,
                x.UnitMeasurementId,
                x.NumericalScaleTypeId,
                x.NumericalScalePrefixId,                
            });

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementId
            Property(x => x.UnitMeasurementId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();            

            //UnitMeasurement
            HasRequired(x => x.UnitMeasurement)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.UnitMeasurementTypeId,
                    x.UnitMeasurementId,                    
                })
                .WillCascadeOnDelete(false);

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScalePrefixId
            Property(x => x.NumericalScalePrefixId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();            

            //NumericalScale
            HasRequired(x => x.NumericalScale)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.NumericalScaleTypeId,
                    x.NumericalScalePrefixId,                    
                })
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}