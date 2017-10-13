using ART.Domotica.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    public class SettingConfiguration : EntityTypeConfiguration<Setting>
    {
        public SettingConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Value
            Property(x => x.Value)
                .HasColumnOrder(1)
                .HasMaxLength(8000)
                .IsRequired();            
        }
    }
}
