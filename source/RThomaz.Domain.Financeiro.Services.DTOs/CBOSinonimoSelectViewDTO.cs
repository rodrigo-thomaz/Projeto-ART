namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class CBOSinonimoSelectViewDTO
    {
        private readonly int _cboSinonimoId;
        private readonly string _titulo;

        public CBOSinonimoSelectViewDTO
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
