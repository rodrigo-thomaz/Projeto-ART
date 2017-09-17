using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class CBOGrandeGrupoConfiguration: EntityTypeConfiguration<CBOGrandeGrupo>
    {
        public CBOGrandeGrupoConfiguration()
        {
            //Primary Keys
            HasKey(x => x.CBOGrandeGrupoId);

            Property(x => x.CBOGrandeGrupoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Codigo
            Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1)
                .IsUnicode(false);

            //Titulo
            Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
