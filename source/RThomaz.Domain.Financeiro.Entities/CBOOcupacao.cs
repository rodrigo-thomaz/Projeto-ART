using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class CBOOcupacao
    {
        #region Primitive Properties

        public int CBOOcupacaoId { get; set; }
        public short CBOFamiliaId { get; set; }        
        public string Codigo { get; set; }
        public string Titulo { get; set; }

        #endregion

        #region NotMapped Properties

        public string TituloExibicao
        {
            get
            {
                return string.Format("{0} - {1}", Codigo, Titulo);
            }
        }

        #endregion

        #region Navigation Properties

        public CBOFamilia CBOFamilia { get; set; }
        public ICollection<CBOSinonimo> CBOSinonimos { get; set; }
        public ICollection<PessoaFisica> PessoasFisicas { get; set; }

        #endregion
    }
}
