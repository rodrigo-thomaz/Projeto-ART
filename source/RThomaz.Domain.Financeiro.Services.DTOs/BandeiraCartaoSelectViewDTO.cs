namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class BandeiraCartaoSelectViewDTO
    {
        private readonly long _bandeiraCartaoId;
        private readonly string _nome;
        private readonly string _logoStorageObject;

        public BandeiraCartaoSelectViewDTO
            (
                  long bandeiraCartaoId
                , string nome
                , string logoStorageObject                
            )
        {
            _bandeiraCartaoId = bandeiraCartaoId;
            _nome = nome;
            _logoStorageObject = logoStorageObject;
        }

        public long BandeiraCartaoId { get { return _bandeiraCartaoId; } }
        public string Nome { get { return _nome; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
    }
}