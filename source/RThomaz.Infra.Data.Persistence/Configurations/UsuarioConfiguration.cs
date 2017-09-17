using System.Data.Entity.ModelConfiguration;
using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
           //Primary Keys
            HasKey(x => new 
            {
                x.UsuarioId, 
            });

            //UsuarioId
            Property(x => x.UsuarioId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Aplicacao
            HasRequired(x => x.Aplicacao)
                .WithMany(x => x.Usuarios)
                .HasForeignKey(x => x.AplicacaoId)
                .WillCascadeOnDelete(false);

            ////Aplicacao
            //HasRequired(x => x.ApplicationUser)
            //    .WithMany(x => x.Usuarios)
            //    .HasForeignKey(x => x.ApplicationUserId)
            //    .WillCascadeOnDelete(false);

            //Email
            Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(250);

            //Senha
            Property(x => x.Senha)
                .IsRequired()
                .HasMaxLength(50);

            //Descrição
            Property(x => x.Descricao)
                .IsOptional()
                .HasMaxLength(4000);
        }
    }
}
