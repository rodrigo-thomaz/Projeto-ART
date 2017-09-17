using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PessoaEmailConfiguration : EntityTypeConfiguration<PessoaEmail>
    {
        public PessoaEmailConfiguration()
        {
           //Primary Keys
            HasKey(x => new 
            { 
                x.PessoaEmailId,
                x.PessoaId,
                x.TipoPessoa,
            });

            //PessoaEmailId
            Property(x => x.PessoaEmailId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //PessoaId
            Property(x => x.PessoaId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoPessoa
            Property(x => x.TipoPessoa)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Pessoa
            HasRequired(x => x.Pessoa)
                .WithMany(x => x.Emails)
                .HasForeignKey(x => new
                {
                    x.PessoaId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //TipoEmail
            HasRequired(x => x.TipoEmail)
                .WithMany(x => x.PessoaEmails)
                .HasForeignKey(x => new 
                {
                    x.TipoEmailId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //Email
            Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
