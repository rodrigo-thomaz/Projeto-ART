namespace ART.Domotica.Repository.Configurations.SI
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities.SI;

    public class NumericalScaleConfiguration : EntityTypeConfiguration<NumericalScale>
    {
        #region Constructors

        public NumericalScaleConfiguration()
        {
            ToTable("NumericalScale", "SI");

            //Primary Keys
            HasKey(x => new
            {
                x.NumericalScalePrefixId,
                x.NumericalScaleTypeId,
            });

            //NumericalScalePrefixId
            Property(x => x.NumericalScalePrefixId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScalePrefix
            HasRequired(x => x.NumericalScalePrefix)
                .WithMany(x => x.NumericalScales)
                .HasForeignKey(x => x.NumericalScalePrefixId)
                .WillCascadeOnDelete(false);

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScaleType
            HasRequired(x => x.NumericalScaleType)
                .WithMany(x => x.NumericalScales)
                .HasForeignKey(x => x.NumericalScaleTypeId)
                .WillCascadeOnDelete(false);

            //Name
            Property(x => x.Name)
                .HasColumnOrder(2)
                .HasMaxLength(30)
                .IsRequired();

            //Base
            Property(x => x.ScientificNotationBase)
                .HasColumnOrder(3)
                .HasPrecision(24, 12)
                .IsRequired();

            //Exponent
            Property(x => x.ScientificNotationExponent)
                .HasColumnOrder(4)
                .HasPrecision(24, 12)
                .IsRequired();
        }

        #endregion Constructors
    }
}