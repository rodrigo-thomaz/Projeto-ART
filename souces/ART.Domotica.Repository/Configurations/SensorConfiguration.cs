namespace ART.Domotica.Repository.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SensorConfiguration : EntityTypeConfiguration<Sensor>
    {
        #region Constructors

        public SensorConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //Label
            Property(x => x.Label)
                .HasColumnOrder(1)
                .HasMaxLength(50)
                .IsRequired();            

            //SensorDatasheetId
            Property(x => x.Id)
                .HasColumnOrder(2)
                .IsRequired();

            //SensorDatasheet
            HasRequired(x => x.SensorDatasheet)
                .WithMany(x => x.Sensors)
                .HasForeignKey(x => new
                {
                    x.SensorDatasheetId,
                    x.SensorTypeId,
                })
                .WillCascadeOnDelete(false);

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(3)
                .IsRequired();
        }

        #endregion Constructors
    }
}