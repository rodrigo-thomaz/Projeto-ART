using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class CBOFamiliaConfiguration: EntityTypeConfiguration<CBOFamilia>
    {
        public CBOFamiliaConfiguration()
        {
            //Primary Keys
            HasKey(x => x.CBOFamiliaId);

            Property(x => x.CBOFamiliaId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //CBOSubGrupoId
            HasRequired(x => x.CBOSubGrupo)
               .WithMany(x => x.CBOFamilias)
               .HasForeignKey(x => x.CBOSubGrupoId)
               .WillCascadeOnDelete(false);

            //Codigo
            Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(4)
                .IsUnicode(false);

            //Titulo
            Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
