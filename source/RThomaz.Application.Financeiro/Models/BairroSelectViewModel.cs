namespace RThomaz.Application.Financeiro.Models
{
    public class BairroSelectViewModel
    {
        private readonly long _bairroId;
        private readonly CidadeSelectViewModel _cidade;
        private readonly string _nome;
        private readonly string _nomeAbreviado;

        public BairroSelectViewModel(
                  long bairroId
                , CidadeSelectViewModel cidade
                , string nome
                , string nomeAbreviado
            )
        {
            _bairroId = bairroId;
            _cidade = cidade;
            _nome = nome;
            _nomeAbreviado = nomeAbreviado;
        }

        public long BairroId { get { return _bairroId; } }
        public CidadeSelectViewModel Cidade { get { return _cidade; } }
        public string Nome { get { return _nome; } }
        public string NomeAbreviado { get { return _nomeAbreviado; } }
    }
}