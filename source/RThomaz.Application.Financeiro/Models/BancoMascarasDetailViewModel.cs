namespace RThomaz.Application.Financeiro.Models
{
    public class BancoMascarasDetailViewModel
    {
        #region private fields

        private string _numeroAgencia;
        private string _numeroContaCorrente;
        private string _numeroContaPoupanca;

        #endregion

        #region constructors

        public BancoMascarasDetailViewModel
        (
              string numeroAgencia
            , string numeroContaCorrente
            , string numeroContaPoupanca
        )
        {
            _numeroAgencia = numeroAgencia;
            _numeroContaCorrente = numeroContaCorrente;
            _numeroContaPoupanca = numeroContaPoupanca;
        }

        #endregion

        /// <summary>
        /// Máscara do número das agências do banco
        /// </summary>
        public string NumeroAgencia { get { return _numeroAgencia; } }

        /// <summary>
        /// Máscara do número das contas corrente do banco
        /// </summary>
        public string NumeroContaCorrente { get { return _numeroContaCorrente; } }

        /// <summary>
        /// Máscara do número das contas poupança do banco
        /// </summary>
        public string NumeroContaPoupanca { get { return _numeroContaPoupanca; } }
    }
}