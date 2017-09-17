using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PessoaHomePageConfiguration : EntityTypeConfiguration<PessoaHomePage>
    {
        public PessoaHomePageConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.PessoaHomePageId,
                x.PessoaId,
                x.TipoPessoa,
            });

            //PessoaHomePageId
            Property(x => x.PessoaHomePageId)
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
                .WithMany(x => x.HomePages)
                .HasForeignKey(x => new
                {
                    x.PessoaId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //TipoHomePage
            HasRequired(x => x.TipoHomePage)
                .WithMany(x => x.PessoaHomePages)
                .HasForeignKey(x => new
                {
                    x.TipoHomePageId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //HomePage
            Property(x => x.HomePage)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
