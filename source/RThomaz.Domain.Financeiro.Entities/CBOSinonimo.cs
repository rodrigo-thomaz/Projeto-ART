using System.Collections.Generic;
namespace RThomaz.Domain.Financeiro.Entities
{
    public class CBOSinonimo
    {
        #region Primitive Properties

        public int CBOSinonimoId { get; set; }
        public int CBOOcupacaoId { get; set; }        
        public string Titulo { get; set; }

        #endregion

        #region Navigation Properties

        public CBOOcupacao CBOOcupacao { get; set; }
        public ICollection<PessoaFisica> PessoasFisicas { get; set; }

        #endregion
    }
}
