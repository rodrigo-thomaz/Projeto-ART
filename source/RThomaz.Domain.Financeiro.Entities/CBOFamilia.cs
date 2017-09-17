using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class CBOFamilia
    {
        #region Primitive Properties

        public short CBOFamiliaId { get; set; }
        public short CBOSubGrupoId { get; set; }        
        public string Codigo { get; set; }
        public string Titulo { get; set; }

        #endregion

        #region Navigation Properties

        public CBOSubGrupo CBOSubGrupo { get; set; }
        public ICollection<CBOOcupacao> CBOOcupacoes { get; set; }

        #endregion
    }
}
