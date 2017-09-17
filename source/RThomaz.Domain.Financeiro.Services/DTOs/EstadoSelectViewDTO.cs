namespace RThomaz.Business.DTOs
{
    public class EstadoMasterDTO
    {
        private readonly long _estadoId;
        private readonly string _paisBandeiraStorageObject;
        private readonly string _paisNome;
        private readonly string _nome;
        private readonly string _sigla;

        public EstadoMasterDTO
            (
                  long estadoId
                , string paisBandeiraStorageObject
                , string paisNome                
                , string nome
                , string sigla
            )
        {
            _estadoId = estadoId;
            _paisBandeiraStorageObject = paisBandeiraStorageObject;
            _paisNome = paisNome;
            _nome = nome;
            _sigla = sigla;
        }

        public long EstadoId { get { return _estadoId; } }
        public string PaisBandeiraStorageObject { get { return _paisBandeiraStorageObject; } }
        public string PaisNome { get { return _paisNome; } }
        public string Nome { get { return _nome; } }
        public string Sigla { get { return _sigla; } }
    }
}