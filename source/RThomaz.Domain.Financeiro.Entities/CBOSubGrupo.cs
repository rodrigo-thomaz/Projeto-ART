using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class CBOSubGrupo
    {
        #region Primitive Properties

        public short CBOSubGrupoId { get; set; }
        public short CBOSubGrupoPrincipalId { get; set; }        
        public string Codigo { get; set; }
        public string Titulo { get; set; }

        #endregion

        #region Navigation Properties

        public CBOSubGrupoPrincipal CBOSubGrupoPrincipal { get; set; }
        public ICollection<CBOFamilia> CBOFamilias { get; set; }

        #endregion
    }
}
