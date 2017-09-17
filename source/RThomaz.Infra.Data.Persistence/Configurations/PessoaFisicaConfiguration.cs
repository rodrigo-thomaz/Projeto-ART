using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PessoaFisicaConfiguration : EntityTypeConfiguration<PessoaFisica>
    {
        public PessoaFisicaConfiguration()
        {
            ToTable("PessoaFisica");

           //Primary Keys
            HasKey(x => new 
            { 
                x.PessoaId,
                x.TipoPessoa,
            });

            //PessoaId
            Property(x => x.PessoaId)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //TipoPessoa
            Property(x => x.TipoPessoa)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //PrimeiroNome
            Property(x => x.PrimeiroNome)
                .IsRequired()
                .HasMaxLength(250);

            //NomeDoMeio
            Property(x => x.NomeDoMeio)
                .IsOptional()
                .HasMaxLength(250);

            //Sobrenome
            Property(x => x.Sobrenome)
                .IsOptional()
                .HasMaxLength(250);

            //Sexo
            Property(x => x.Sexo)
                .IsRequired();

            //EstadoCivil
            Property(x => x.EstadoCivil)
                .IsOptional();

            //CPF
            Property(x => x.CPF)
                .IsOptional()
                .HasMaxLength(11);

            //RG
            Property(x => x.RG)
                .IsOptional()
                .HasMaxLength(9);

            //OrgaoEmissor
            Property(x => x.OrgaoEmissor)
                .IsOptional()
                .HasMaxLength(100);

            //DataNascimento
            Property(x => x.DataNascimento)
                .IsOptional();

            //Naturalidade
            Property(x => x.Naturalidade)
                .IsOptional()
                .HasMaxLength(100);

            //Nacionalidade
            Property(x => x.Nacionalidade)
                .IsOptional()
                .HasMaxLength(100);

            //NomeCompleto
            Ignore(x => x.NomeCompleto);

            //CBOOcupacaoId
            HasOptional(x => x.CBOOcupacao)
                .WithMany(x => x.PessoasFisicas)
                .HasForeignKey(x => x.CBOOcupacaoId)
                .WillCascadeOnDelete(false);

            //CBOSinonimoId
            HasOptional(x => x.CBOSinonimo)
                .WithMany(x => x.PessoasFisicas)
                .HasForeignKey(x => new
                {
                    x.CBOSinonimoId,
                    x.CBOOcupacaoId,
                })
                .WillCascadeOnDelete(false);
        }
    }
}
