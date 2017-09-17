namespace RThomaz.Application.Financeiro.Models
{
    public class PaisSelectViewModel
    {
        private readonly long _paisId;
        private readonly string _nome;
        private readonly string _iso2;
        private readonly string _iso3;
        private readonly string _numCode;
        private readonly string _ddi;
        private readonly string _ccTLD;
        private readonly string _bandeiraStorageObject;

        public PaisSelectViewModel(long paisId, string nome, string iso2, string iso3, string numCode, string ddi, string ccTLD, string bandeiraStorageObject)
        {
            _paisId = paisId;
            _nome = nome;
            _iso2 = iso2;
            _iso3 = iso3;
            _numCode = numCode;
            _ddi = ddi;
            _ccTLD = ccTLD;
            _bandeiraStorageObject = bandeiraStorageObject;
        }

        public long PaisId { get { return _paisId; } }
        public string Nome { get { return _nome; } }

        public string ISO2 { get { return _iso2; } }

        public string ISO3 { get { return _iso3; } }

        public string NumCode { get { return _numCode; } }

        public string DDI { get { return _ddi; } }

        public string ccTLD { get { return _ccTLD; } }

        public string BandeiraStorageObject { get { return _bandeiraStorageObject; } }
    }
}