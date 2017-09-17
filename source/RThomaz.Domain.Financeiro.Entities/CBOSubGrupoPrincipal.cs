using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class CBOSubGrupoPrincipal
    {
        #region Primitive Properties

        public short CBOSubGrupoPrincipalId { get; set; }
        public short CBOGrandeGrupoId { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }

        #endregion

        #region Navigation Properties

        public CBOGrandeGrupo CBOGrandeGrupo { get; set; }
        public ICollection<CBOSubGrupo> CBOSubGrupos { get; set; }

        #endregion
    }
}
