using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class CBOOcupacaoConfiguration: EntityTypeConfiguration<CBOOcupacao>
    {
        public CBOOcupacaoConfiguration()
        {
            //Primary Keys
            HasKey(x => x.CBOOcupacaoId);

            Property(x => x.CBOOcupacaoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //CBOSubGrupoId
            HasRequired(x => x.CBOFamilia)
               .WithMany(x => x.CBOOcupacoes)
               .HasForeignKey(x => x.CBOFamiliaId)
               .WillCascadeOnDelete(false);

            //Codigo
            Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(6)
                .IsUnicode(false);

            //Titulo
            Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(250);

            //TituloExibicao
            Ignore(x => x.TituloExibicao);
        }
    }
}
