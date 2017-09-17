namespace RThomaz.Application.Financeiro.Models
{
    public class EstadoSelectViewModel
    {
        private readonly long _estadoId;
        private readonly PaisSelectViewModel _pais;
        private readonly string _nome;
        private readonly string _sigla;

        public EstadoSelectViewModel(
                  long estadoId
                , PaisSelectViewModel pais
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
        public PaisSelectViewModel Pais { get { return _pais; } }
        public string Nome { get { return _nome; } }
        public string Sigla { get { return _sigla; } }
    }
}