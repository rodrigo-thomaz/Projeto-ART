using RThomaz.Domain.Financeiro.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class CBOSinonimoConfiguration: EntityTypeConfiguration<CBOSinonimo>
    {
        public CBOSinonimoConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.CBOSinonimoId,
                x.CBOOcupacaoId
            });
            Property(x => x.CBOSinonimoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //CBOSubGrupoId
            HasRequired(x => x.CBOOcupacao)
               .WithMany(x => x.CBOSinonimos)
               .HasForeignKey(x => x.CBOOcupacaoId)
               .WillCascadeOnDelete(false);

            //Titulo
            Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
