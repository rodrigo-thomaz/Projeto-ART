using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Aplicacao
    {
        #region Primitive Properties

        public Guid AplicacaoId { get; set; }


        #endregion

        #region Navigation Properties

        public ICollection<Usuario> Usuarios { get; set; }

        #endregion
    }
}
