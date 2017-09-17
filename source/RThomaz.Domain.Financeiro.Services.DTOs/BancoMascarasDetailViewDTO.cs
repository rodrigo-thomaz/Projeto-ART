namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class BancoMascarasDetailViewDTO
    {
        private readonly string _numeroAgencia;
        private readonly string _numeroContaCorrente;
        private readonly string _numeroContaPoupanca;

        public BancoMascarasDetailViewDTO
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

        public string NumeroAgencia { get { return _numeroAgencia; } }
        public string NumeroContaCorrente { get { return _numeroContaCorrente; } }
        public string NumeroContaPoupanca { get { return _numeroContaPoupanca; } }
    }
}
