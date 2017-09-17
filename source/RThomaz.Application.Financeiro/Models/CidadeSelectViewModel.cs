namespace RThomaz.Application.Financeiro.Models
{
    public class CidadeSelectViewModel
    {
        private readonly long _cidadeId;
        private readonly EstadoSelectViewModel _estado;
        private readonly string _nome;
        private readonly string _nomeAbreviado;

        public CidadeSelectViewModel(
                  long cidadeId
                , EstadoSelectViewModel estado
                , string nome
                , string nomeAbreviado
            )
        {
            _cidadeId = cidadeId;
            _estado = estado;
            _nome = nome;
            _nomeAbreviado = nomeAbreviado;
        }

        public long CidadeId { get { return _cidadeId; } }
        public EstadoSelectViewModel Estado { get { return _estado; } }
        public string Nome { get { return _nome; } }
        public string NomeAbreviado { get { return _nomeAbreviado; } }
    }
}