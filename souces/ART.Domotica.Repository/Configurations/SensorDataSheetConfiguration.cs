using ART.Domotica.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    public class SensorDatasheetConfiguration : EntityTypeConfiguration<SensorDatasheet>
    {
        #region Constructors

        public SensorDatasheetConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.Id,
                x.SensorTypeId,
            });

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorTypeId
            Property(x => x.SensorTypeId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorType
            HasRequired(x => x.SensorType)
                .WithMany(x => x.SensorDatasheets)
                .HasForeignKey(x => x.SensorTypeId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}