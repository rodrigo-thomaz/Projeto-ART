namespace ART.Domotica.Repository.Configurations.SI
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities.SI;

    public class NumericalScaleTypeCountryConfiguration : EntityTypeConfiguration<NumericalScaleTypeCountry>
    {
        #region Constructors

        public NumericalScaleTypeCountryConfiguration()
        {
            ToTable("NumericalScaleTypeCountry", "SI");

            //Primary Keys
            HasKey(x => new
            {
                x.NumericalScaleTypeId,
                x.CountryId,
            });

            //NumericalScaleTypeId
            Property(x => x.NumericalScaleTypeId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScaleType
            HasRequired(x => x.NumericalScaleType)
                .WithMany(x => x.NumericalScaleTypeCountries)
                .HasForeignKey(x => x.NumericalScaleTypeId)
                .WillCascadeOnDelete(false);

            //CountryId
            Property(x => x.CountryId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //NumericalScaleType
            HasRequired(x => x.Country)
                .WithMany(x => x.NumericalScaleTypesCountry)
                .HasForeignKey(x => x.CountryId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}