using ART.Domotica.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    public class NumericalScaleCountryConfiguration : EntityTypeConfiguration<NumericalScaleCountry>
    {
        #region Constructors

        public NumericalScaleCountryConfiguration()
        {
            ToTable("NumericalScaleCountry", "SI");

            //Primary Keys
            HasKey(x => new
            {
                x.NumericalScaleId,
                x.CountryId,
            });

            //NumericalScaleId
            Property(x => x.NumericalScaleId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScale
            HasRequired(x => x.NumericalScale)
                .WithMany(x => x.NumericalScaleCountries)
                .HasForeignKey(x => x.NumericalScaleId)
                .WillCascadeOnDelete(false);

            //CountryId
            Property(x => x.CountryId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScale
            HasRequired(x => x.Country)
                .WithMany(x => x.NumericalScalesCountry)
                .HasForeignKey(x => x.CountryId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}