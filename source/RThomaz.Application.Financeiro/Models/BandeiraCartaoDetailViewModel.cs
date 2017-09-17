namespace RThomaz.Application.Financeiro.Models
{
    public class BandeiraCartaoDetailViewModel
    {
        private readonly long _bandeiraCartaoId;
        private readonly string _nome;
        private readonly string _logoStorageObject;
        private readonly bool _ativo;

        public BandeiraCartaoDetailViewModel
            (
                  long bandeiraCartaoId
                , string nome
                , bool ativo
                , string logoStorageObject
            )
        {
            _bandeiraCartaoId = bandeiraCartaoId;
            _nome = nome;
            _ativo = ativo;
            _logoStorageObject = logoStorageObject;
        }

        public long BandeiraCartaoId { get { return _bandeiraCartaoId; } }
        public string Nome { get { return _nome; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
        public bool Ativo { get { return _ativo; } }
    }
}