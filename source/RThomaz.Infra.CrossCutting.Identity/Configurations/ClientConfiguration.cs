using RThomaz.Infra.CrossCutting.Identity.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.CrossCutting.Identity.Configurations
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasMaxLength(255)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Secret
            Property(x => x.Secret)
                .IsRequired()
                .HasMaxLength(255);

            //Name
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            //AllowedOrigin
            Property(x => x.AllowedOrigin)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
