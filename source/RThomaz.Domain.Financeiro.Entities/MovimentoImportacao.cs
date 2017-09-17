using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class MovimentoImportacao
    {
        #region Primitive Properties

        public long MovimentoImportacaoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public DateTime ImportadoEm { get; set; }

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public ICollection<Movimento> Movimentacoes { get; set; }

        #endregion
    }
}
