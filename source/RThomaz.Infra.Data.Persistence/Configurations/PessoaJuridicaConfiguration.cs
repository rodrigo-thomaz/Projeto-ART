using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PessoaJuridicaConfiguration : EntityTypeConfiguration<PessoaJuridica>
    {
        public PessoaJuridicaConfiguration()
        {
            ToTable("PessoaJuridica");

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

            //RazaoSocial
            Property(x => x.RazaoSocial)
                .IsRequired()
                .HasMaxLength(250);

            //NomeFantasia
            Property(x => x.NomeFantasia)
                .IsRequired()
                .HasMaxLength(250);

            //CNPJ
            Property(x => x.CNPJ)
                .IsOptional()
                .HasMaxLength(14);

            //InscricaoEstadual
            Property(x => x.InscricaoEstadual)
                .IsOptional()
                .HasMaxLength(12);

            //InscricaoMunicipal
            Property(x => x.InscricaoMunicipal)
                .IsOptional()
                .HasMaxLength(20);
        }
    }
}
