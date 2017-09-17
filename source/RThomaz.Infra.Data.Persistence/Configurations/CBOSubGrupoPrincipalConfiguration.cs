using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class CBOSubGrupoPrincipalConfiguration: EntityTypeConfiguration<CBOSubGrupoPrincipal>
    {
        public CBOSubGrupoPrincipalConfiguration()
        {
            //Primary Keys
            HasKey(x => x.CBOSubGrupoPrincipalId);

            Property(x => x.CBOSubGrupoPrincipalId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //CBOGrandeGrupoId
            HasRequired(x => x.CBOGrandeGrupo)
               .WithMany(x => x.CBOSubGruposPrincipais)
               .HasForeignKey(x => x.CBOGrandeGrupoId)
               .WillCascadeOnDelete(false);

            //Codigo
            Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2)
                .IsUnicode(false);

            //Titulo
            Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
