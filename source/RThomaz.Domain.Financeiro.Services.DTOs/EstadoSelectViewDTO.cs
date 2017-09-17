namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class EstadoSelectViewDTO
    {
        private readonly long _estadoId;
        private readonly PaisSelectViewDTO _pais;
        private readonly string _nome;
        private readonly string _sigla;

        public EstadoSelectViewDTO
            (
                  long estadoId
                , PaisSelectViewDTO pais
                , string nome
                , string sigla
            )
        {
            _estadoId = estadoId;
            _pais = pais;
            _nome = nome;
            _sigla = sigla;
        }

        public long EstadoId { get { return _estadoId; } }
        public PaisSelectViewDTO Pais { get { return _pais; } }
        public string Nome { get { return _nome; } }
        public string Sigla { get { return _sigla; } }
    }
}