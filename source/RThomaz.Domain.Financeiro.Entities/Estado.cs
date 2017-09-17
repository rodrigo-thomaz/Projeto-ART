using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
namespace RThomaz.Domain.Financeiro.Entities
{
    public class Estado
    {
        #region Primitive Properties

        public long EstadoId { get; set; }

        public long PaisId { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public TipoOrigemDado TipoOrigemDado { get; set; }

        #endregion

        #region Navigation Properties

        public Pais Pais { get; set; }

        public ICollection<Cidade> Cidades { get; set; }

        #endregion
    }
}
