using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class BancoSelectViewModel
    {
        #region private fields

        private readonly long _bancoId;
        private readonly string _nome;
        private readonly string _numero;
        private readonly string _logoStorageObject;

        #endregion

        #region constructors

        public BancoSelectViewModel
            (
                  long bancoId
                , string nome
                , string numero
                , string logoStorageObject
            )
        {
            _bancoId = bancoId;
            _nome = nome;
            _numero = numero;
            _logoStorageObject = logoStorageObject;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Id do banco
        /// </summary>
        [Required]
        public long BancoId { get { return _bancoId; } }

        /// <summary>
        /// Nome do banco
        /// </summary>
        [Required]
        public string Nome { get { return _nome; } }

        /// <summary>
        /// Número do banco
        /// </summary>
        [Required]
        public string Numero { get { return _numero; } }

        /// <summary>
        /// Nome do logo no storage
        /// </summary>
        public string LogoStorageObject { get { return _logoStorageObject; } } 

        #endregion
    }
}