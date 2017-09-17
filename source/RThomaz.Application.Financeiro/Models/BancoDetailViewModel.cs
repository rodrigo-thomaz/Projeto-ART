using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class BancoDetailViewModel
    {
        #region private fields

        private long _bancoId;
        private string _nome;
        private string _nomeAbreviado;
        private string _numero;
        private string _mascaraNumeroAgencia;
        private string _mascaraNumeroContaCorrente;
        private string _mascaraNumeroContaPoupanca;
        private string _codigoImportacaoOfx;
        private string _site;
        private string _logoStorageObject;
        private string _descricao;
        private bool _ativo;

        #endregion

        #region constructors

        public BancoDetailViewModel
        (
              long bancoId
            , string nome
            , string nomeAbreviado
            , string numero
            , string mascaraNumeroAgencia
            , string mascaraNumeroContaCorrente
            , string mascaraNumeroContaPoupanca
            , string codigoImportacaoOfx
            , string site
            , string logoStorageObject
            , string descricao
            , bool ativo
        )
        {
            _bancoId = bancoId;
            _nome = nome;
            _nomeAbreviado = nomeAbreviado;
            _numero = numero;
            _mascaraNumeroAgencia = mascaraNumeroAgencia;
            _mascaraNumeroContaCorrente = mascaraNumeroContaCorrente;
            _mascaraNumeroContaPoupanca = mascaraNumeroContaPoupanca;
            _codigoImportacaoOfx = codigoImportacaoOfx;
            _site = site;
            _logoStorageObject = logoStorageObject;
            _descricao = descricao;
            _ativo = ativo;
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
        /// Nome abreviado do banco
        /// </summary>
        [Required]
        public string NomeAbreviado { get { return _nomeAbreviado; } }

        /// <summary>
        /// Número do banco
        /// </summary>
        [Required]
        public string Numero { get { return _numero; } }

        /// <summary>
        /// Máscara do número das agências do banco
        /// </summary>
        public string MascaraNumeroAgencia { get { return _mascaraNumeroAgencia; } }

        /// <summary>
        /// Máscara do número das contas corrente do banco
        /// </summary>
        public string MascaraNumeroContaCorrente { get { return _mascaraNumeroContaCorrente; } }

        /// <summary>
        /// Máscara do número das contas poupança do banco
        /// </summary>
        public string MascaraNumeroContaPoupanca { get { return _mascaraNumeroContaPoupanca; } }

        /// <summary>
        /// Código para importação de extrato no formato Ofx
        /// </summary>
        public string CodigoImportacaoOfx { get { return _codigoImportacaoOfx; } }

        /// <summary>
        /// Site do banco
        /// </summary>
        public string Site { get { return _site; } }

        /// <summary>
        /// Nome do logo no storage
        /// </summary>
        public string LogoStorageObject { get { return _logoStorageObject; } }

        /// <summary>
        /// Descrição do banco
        /// </summary>
        public string Descricao { get { return _descricao; } }

        /// <summary>
        /// Status de ativo do banco
        /// </summary>
        [Required]
        public bool Ativo { get { return _ativo; } }

        #endregion
    }
}