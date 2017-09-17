namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class DadoBancarioDTO
    {
        private readonly string _numeroAgencia;
        private readonly string _numeroConta;

        public DadoBancarioDTO
            (
                  string numeroAgencia
                , string numeroConta
            )
        {
            _numeroAgencia = numeroAgencia;
            _numeroConta = numeroConta;
        }

        public string NumeroAgencia { get { return _numeroAgencia; } }
        public string NumeroConta { get { return _numeroConta; } }
    }
}
