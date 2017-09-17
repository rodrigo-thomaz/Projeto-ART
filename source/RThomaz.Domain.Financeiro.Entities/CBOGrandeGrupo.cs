using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class CBOGrandeGrupo
    {
        #region Primitive Properties

        public short CBOGrandeGrupoId { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<CBOSubGrupoPrincipal> CBOSubGruposPrincipais { get; set; }

        #endregion
    }
}
