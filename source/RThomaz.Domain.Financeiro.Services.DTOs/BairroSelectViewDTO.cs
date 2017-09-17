namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class BairroSelectViewDTO
    {
        private readonly long _bairroId;
        private readonly CidadeSelectViewDTO _cidade;
        private readonly string _nome;
        private readonly string _nomeAbreviado;

        public BairroSelectViewDTO
            (
                  long bairroId
                , CidadeSelectViewDTO cidade
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
        public CidadeSelectViewDTO Cidade { get { return _cidade; } }
        public string Nome { get { return _nome; } }
        public string NomeAbreviado { get { return _nomeAbreviado; } }
    }
}