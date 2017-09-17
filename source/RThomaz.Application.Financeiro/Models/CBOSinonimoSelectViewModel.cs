namespace RThomaz.Application.Financeiro.Models
{
    public class CBOSinonimoSelectViewModel
    {
        private readonly int _cboSinonimoId;
        private readonly string _titulo;

        public CBOSinonimoSelectViewModel
            (
                  int cboSinonimoId
                , string titulo               
            )
        {
            _cboSinonimoId = cboSinonimoId;
            _titulo = titulo;
        }

        public int CBOSinonimoId { get { return _cboSinonimoId; } }
        public string Titulo { get { return _titulo; } }
    }
}
