namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class TempSensorRangeConfiguration : EntityTypeConfiguration<TempSensorRange>
    {
        #region Constructors

        public TempSensorRangeConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Min
            Property(x => x.Min)
                .HasColumnOrder(1)
                .IsRequired();

            //Max
            Property(x => x.Max)
                .HasColumnOrder(2)
                .IsRequired();
        }

        #endregion Constructors
    }
}