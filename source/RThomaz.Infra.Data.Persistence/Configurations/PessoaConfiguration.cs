using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PessoaConfiguration : EntityTypeConfiguration<Pessoa>
    {
        public PessoaConfiguration()
        {
           //Primary Keys
            HasKey(x => new 
            { 
                x.PessoaId,
                x.TipoPessoa,
            });

            //PessoaId
            Property(x => x.PessoaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //TipoPessoa
            Property(x => x.TipoPessoa)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany()
                .WillCascadeOnDelete(false);

            //Descrição
            Property(x => x.Descricao)
                .IsOptional()
                .HasMaxLength(4000);
        }
    }
}
