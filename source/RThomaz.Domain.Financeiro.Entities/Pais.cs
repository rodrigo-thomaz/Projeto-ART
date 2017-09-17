using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Pais
    {
        #region Primitive Properties

        public long PaisId { get; set; }

        public string Nome { get; set; }

        public string ISO2 { get; set; }

        public string ISO3 { get; set; }

        public string NumCode { get; set; }

        public string DDI { get; set; }

        public string ccTLD { get; set; }

        public string BandeiraStorageObject { get; set; }

        public TipoOrigemDado TipoOrigemDado { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<Estado> Estados { get; set; }

        #endregion
    }
}