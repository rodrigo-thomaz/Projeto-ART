namespace RThomaz.Business.DTOs
{
    public class CidadeMasterDTO
    {
        private readonly long _cidadeId;
        private readonly string _paisNome;
        private readonly string _estadoNome;
        private readonly string _nome;
        private readonly string _numCode;
        private readonly string _cep;

        public CidadeMasterDTO
            (
                  long cidadeId
                , string paisNome
                , string estadoNome
                , string nome
                , string numCode
                , string cep
            )
        {
            _cidadeId = cidadeId;
            _paisNome = paisNome;
            _estadoNome = estadoNome;
            _nome = nome;
            _numCode = numCode;
            _cep = cep;
        }

        public long CidadeId { get { return _cidadeId; } }
        public string PaisNome { get { return _paisNome; } }
        public string EstadoNome { get { return _estadoNome; } }
        public string Nome { get { return _nome; } }
        public string NumCode { get { return _numCode; } }
        public string CEP { get { return _cep; } }
    }
}