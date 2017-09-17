using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class CBOSubGrupoConfiguration: EntityTypeConfiguration<CBOSubGrupo>
    {
        public CBOSubGrupoConfiguration()
        {
            //Primary Keys
            HasKey(x => x.CBOSubGrupoId);

            Property(x => x.CBOSubGrupoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //CBOSubGrupoPrincipalId
            HasRequired(x => x.CBOSubGrupoPrincipal)
               .WithMany(x => x.CBOSubGrupos)
               .HasForeignKey(x => x.CBOSubGrupoPrincipalId)
               .WillCascadeOnDelete(false);

            //Codigo
            Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3)
                .IsUnicode(false);

            //Titulo
            Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
