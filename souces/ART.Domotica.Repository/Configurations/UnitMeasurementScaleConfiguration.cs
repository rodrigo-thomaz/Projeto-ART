using ART.Domotica.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    public class UnitMeasurementScaleConfiguration : EntityTypeConfiguration<UnitMeasurementScale>
    {
        #region Constructors

        public UnitMeasurementScaleConfiguration()
        {
            ToTable("UnitMeasurementScale", "SI");

            //Primary Keys
            HasKey(x => new
            {
                x.UnitMeasurementId,
                x.UnitMeasurementTypeId,
                x.NumericalScalePrefixId,
                x.NumericalScaleTypeId,
            });

            //UnitMeasurementId
            Property(x => x.UnitMeasurementId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurementTypeId
            Property(x => x.UnitMeasurementTypeId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //UnitMeasurement
            HasRequired(x => x.UnitMeasurement)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.UnitMeasurementId,
                    x.UnitMeasurementTypeId,
                })
                .WillCascadeOnDelete(false);

            //UnitMeasurementType
            HasRequired(x => x.UnitMeasurementType)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => x.UnitMeasurementTypeId)
                .WillCascadeOnDelete(false);

            //NumericalScalePrefixId
            Property(x => x.NumericalScalePrefixId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();            

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScale
            HasRequired(x => x.NumericalScale)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => new
                {
                    x.NumericalScalePrefixId,
                    x.NumericalScaleTypeId,
                })
                .WillCascadeOnDelete(false);

            //NumericalScalePrefix
            HasRequired(x => x.NumericalScalePrefix)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => x.NumericalScalePrefixId)
                .WillCascadeOnDelete(false);

            //NumericalScaleType
            HasRequired(x => x.NumericalScaleType)
                .WithMany(x => x.UnitMeasurementScales)
                .HasForeignKey(x => x.NumericalScaleTypeId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}