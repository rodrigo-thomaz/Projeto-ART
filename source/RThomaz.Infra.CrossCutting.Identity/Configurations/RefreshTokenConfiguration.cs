using RThomaz.Infra.CrossCutting.Identity.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.CrossCutting.Identity.Configurations
{
    public class RefreshTokenConfiguration : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Subject
            Property(x => x.Subject)
                .IsRequired()
                .HasMaxLength(50);

            //ClientId
            Property(x => x.ClientId)
                .IsRequired()
                .HasMaxLength(50);

            //ProtectedTicket
            Property(x => x.ProtectedTicket)
                .IsRequired()
                .HasMaxLength(8000);
        }
    }
}
