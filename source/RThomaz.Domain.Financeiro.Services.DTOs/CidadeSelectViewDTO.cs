namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class CidadeSelectViewDTO
    {
        private readonly long _cidadeId;
        private readonly EstadoSelectViewDTO _estado;
        private readonly string _nome;
        private readonly string _nomeAbreviado;

        public CidadeSelectViewDTO
            (
                  long cidadeId
                , EstadoSelectViewDTO estado
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
        public EstadoSelectViewDTO Estado { get { return _estado; } }
        public string Nome { get { return _nome; } }
        public string NomeAbreviado { get { return _nomeAbreviado; } }
    }
}