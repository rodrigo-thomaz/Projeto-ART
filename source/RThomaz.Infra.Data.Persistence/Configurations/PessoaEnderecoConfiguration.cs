using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class PessoaEnderecoConfiguration : EntityTypeConfiguration<PessoaEndereco>
    {
        public PessoaEnderecoConfiguration()
        {
           //Primary Keys
            HasKey(x => new 
            { 
                x.PessoaEnderecoId,
                x.PessoaId,
                x.TipoPessoa,
            });

            //PessoaEnderecoId
            Property(x => x.PessoaEnderecoId)
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
                .WithMany(x => x.Enderecos)
                .HasForeignKey(x => new
                {
                    x.PessoaId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //TipoEndereco
            HasRequired(x => x.TipoEndereco)
                .WithMany(x => x.PessoaEnderecos)
                .HasForeignKey(x => new 
                {
                    x.TipoEnderecoId,
                    x.TipoPessoa,
                })
                .WillCascadeOnDelete(false);

            //Cep
            Property(x => x.Cep)
                .IsOptional()
                .HasMaxLength(20);

            //Logradouro
            Property(x => x.Logradouro)
                .IsOptional()
                .HasMaxLength(255);

            //Numero
            Property(x => x.Numero)
                .IsOptional()
                .HasMaxLength(10);

            //Complemento
            Property(x => x.Complemento)
                .IsOptional()
                .HasMaxLength(255);

            //Bairro
            HasRequired(x => x.Bairro)
                .WithMany(x => x.PessoaEnderecos)
                .HasForeignKey(x => x.BairroId)
                .WillCascadeOnDelete(false);

            //Longitude
            Property(x => x.Longitude)
                .IsRequired();

            //Latitude
            Property(x => x.Latitude)
                .IsRequired();
        }
    }
}
