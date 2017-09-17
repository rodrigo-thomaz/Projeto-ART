using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class ContaEspecieConfiguration : EntityTypeConfiguration<ContaEspecie>
    {
        public ContaEspecieConfiguration()
        {
            ToTable("ContaEspecie");

            //Primary Keys
            HasKey(x => new
            {
                x.ContaId,
                x.TipoConta,
            });

            //ContaId
            Property(x => x.ContaId)
                .IsRequired();

            //AplicacaoId
            Property(x => x.AplicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //TipoConta
            Property(x => x.TipoConta)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Nome
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
