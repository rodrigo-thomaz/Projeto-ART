using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Cidade
    {
        #region Primitive Properties

        public long CidadeId { get; set; }

        public long EstadoId { get; set; }

        public string Nome { get; set; }

        public string NomeAbreviado { get; set; }

        #endregion

        #region Navigation Properties

        public Estado Estado { get; set; }

        public ICollection<Bairro> Bairros { get; set; }

        #endregion
    }
}
