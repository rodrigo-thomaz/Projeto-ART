using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PessoaTelefoneConfiguration : EntityTypeConfiguration<PessoaTelefone>
    {
        public PessoaTelefoneConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.PessoaTelefoneId,
                x.PessoaId,
                x.TipoPessoa,
            });

            //PessoaTelefoneId
            Property(x => x.PessoaTelefoneId)
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
                .WithMany(x => x.Telefones)
                .HasForeignKey(x => new
                {
                    x.PessoaId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //TipoTelefone
            HasRequired(x => x.TipoTelefone)
                .WithMany(x => x.PessoaTelefones)
                .HasForeignKey(x => new
                {
                    x.TipoTelefoneId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //Telefone
            Property(x => x.Telefone)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
