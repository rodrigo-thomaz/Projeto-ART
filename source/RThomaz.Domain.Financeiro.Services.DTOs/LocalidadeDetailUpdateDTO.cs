namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LocalidadeDetailUpdateDTO
    {
        private readonly string _bairroNome;
        private readonly string _bairroNomeAbreviado;
        private readonly string _cidadeNome;
        private readonly string _cidadeNomeAbreviado;
        private readonly string _estadoNome;
        private readonly string _estadoSigla;
        private readonly string _paisNome;
        private readonly string _paisISO2;

        public LocalidadeDetailUpdateDTO
            (
                  string bairroNome
                , string bairroNomeAbreviado
                , string cidadeNome
                , string cidadeNomeAbreviado
                , string estadoNome
                , string estadoSigla
                , string paisNome
                , string paisISO2
            )
        {
            _bairroNome = bairroNome;
            _bairroNomeAbreviado = bairroNomeAbreviado;            
            _cidadeNome = cidadeNome;
            _cidadeNomeAbreviado = cidadeNomeAbreviado;
            _estadoNome = estadoNome;
            _estadoSigla = estadoSigla;
            _paisNome = paisNome;
            _paisISO2 = paisISO2;
        }

        public string BairroNome { get { return _bairroNome; } }
        public string BairroNomeAbreviado { get { return _bairroNomeAbreviado; } }
        public string CidadeNome { get { return _cidadeNome; } }
        public string CidadeNomeAbreviado { get { return _cidadeNomeAbreviado; } }
        public string EstadoNome { get { return _estadoNome; } }
        public string EstadoSigla { get { return _estadoSigla; } }
        public string PaisNome { get { return _paisNome; } }
        public string PaisISO2 { get { return _paisISO2; } }
    }
}