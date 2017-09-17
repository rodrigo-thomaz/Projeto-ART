using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RThomaz.Domain.Financeiro.Entities;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PerfilConfiguration : EntityTypeConfiguration<Perfil>
    {
        public PerfilConfiguration()
        {
           //Primary Keys
            HasKey(x => new 
            {
                x.UsuarioId, 
            });           

            Property(x => x.UsuarioId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(x => x.Usuario)
                .WithRequiredDependent(x => x.Perfil);

            //NomeExibicao
            Property(x => x.NomeExibicao)
                .IsRequired()
                .HasMaxLength(250);

            //LogoStorageObject
            Property(x => x.AvatarStorageObject)
                .IsOptional()
                .HasMaxLength(255);
        }
    }
}
