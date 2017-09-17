using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class LancamentoConfiguration : EntityTypeConfiguration<Lancamento>
    {
        public LancamentoConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.LancamentoId,
                x.TipoTransacao,
            });

            //LancamentoId
            Property(x => x.LancamentoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //TipoTransacao
            Property(x => x.TipoTransacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Aplicacao
            HasRequired(u => u.Aplicacao)
                .WithMany()
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasOptional(x => x.Pessoa)
                .WithMany(x => x.Lancamentos)
                .HasForeignKey(x => new 
                { 
                    x.PessoaId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasOptional(x => x.Transferencia)
                .WithMany(x => x.Lancamentos)
                .HasForeignKey(x => new
                {
                    x.TransferenciaId,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasOptional(x => x.Programacao)
                .WithMany(x => x.Lancamentos)
                .HasForeignKey(x => new
                {
                    x.ProgramacaoId,
                    x.TipoProgramacao,
                    x.TipoTransacao,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasRequired(x => x.Conta)
                .WithMany(x => x.Lancamentos)
                .HasForeignKey(x => new
                {
                    x.ContaId,
                    x.TipoConta,
                })
                .WillCascadeOnDelete(false);

            //Foreing Keys            
            HasOptional(x => x.TransferenciaProgramada)
                .WithMany(x => x.Lancamentos)
                .HasForeignKey(x => new
                {
                    x.TransferenciaProgramadaId,
                })
                .WillCascadeOnDelete(false);

            //ValorVencimento
            Property(x => x.ValorVencimento)
                .IsRequired()
                .HasPrecision(18, 2);

            //DataVencimento
            Property(x => x.DataVencimento)
                .IsRequired();       

            //Historico
            Property(x => x.Historico)
                .HasMaxLength(500)
                .IsRequired();

            //Observacao
            Property(x => x.Observacao)
                .HasMaxLength(4000)
                .IsOptional();

            //Numero
            Property(x => x.Numero)
                .HasMaxLength(50)
                .IsOptional();
        }
    }
}
