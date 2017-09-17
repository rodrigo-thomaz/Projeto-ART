using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class ProgramacaoConfiguration : EntityTypeConfiguration<Programacao>
    {
        public ProgramacaoConfiguration()
        {
            //Primary Keys
            HasKey(x => new 
            { 
                x.ProgramacaoId,
                x.TipoProgramacao,
                x.TipoTransacao,
            });

            //ProgramacaoId
            Property(x => x.ProgramacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoProgramacao
            Property(x => x.TipoProgramacao)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TipoTransacao
            Property(x => x.TipoTransacao)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany()
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasOptional(x => x.Pessoa)
                .WithMany(x => x.Programacoes)
                .HasForeignKey(x => new
                {
                    x.PessoaId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasRequired(x => x.Conta)
                .WithMany(x => x.Programacoes)
                .HasForeignKey(x => new
                {
                    x.ContaId,
                    x.TipoConta,
                })
                .WillCascadeOnDelete(false);
        }
    }
}
