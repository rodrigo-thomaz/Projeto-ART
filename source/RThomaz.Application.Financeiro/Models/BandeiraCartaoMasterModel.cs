namespace RThomaz.Application.Financeiro.Models
{
    public class BandeiraCartaoMasterModel
    {
        private readonly long _bandeiraCartaoId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _logoStorageObject;

        public BandeiraCartaoMasterModel(
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
        public bool Ativo { get { return _ativo; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
    }
}