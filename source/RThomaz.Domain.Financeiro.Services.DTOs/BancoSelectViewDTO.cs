namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class BancoSelectViewDTO
    {
        private readonly long _bancoId;
        private readonly string _nome;
        private readonly string _numero;
        private readonly string _logoStorageObject;

        public BancoSelectViewDTO
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

        public long BancoId { get { return _bancoId; } }
        public string Nome { get { return _nome; } }
        public string Numero { get { return _numero; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
    }
}
