namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorChartLimiterConfiguration : EntityTypeConfiguration<SensorChartLimiter>
    {
        #region Constructors

        public SensorChartLimiterConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorBase
            HasRequired(x => x.SensorBase)
               .WithRequiredDependent(x => x.SensorChartLimiter);

            //Min
            Property(x => x.Min)
                .HasColumnOrder(1)
                .HasPrecision(7, 4)
                .IsRequired();

            //Max
            Property(x => x.Max)
                .HasColumnOrder(2)
                .HasPrecision(7, 4)
                .IsRequired();
        }

        #endregion Constructors
    }
}