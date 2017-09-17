namespace RThomaz.Business.DTOs
{
    public class CBOSinonimoMasterDTO
    {
        private readonly int _cboSinonimoId;
        private readonly string _codigo;
        private readonly string _titulo;

        public CBOSinonimoMasterDTO
            (
                  int cboSinonimoId
                , string codigo
                , string titulo               
            )
        {
            _cboSinonimoId = cboSinonimoId;
            _codigo = codigo;
            _titulo = titulo;
        }

        public long CBOSinonimoId { get { return _cboSinonimoId; } }
        public string Codigo { get { return _codigo; } }
        public string Titulo { get { return _titulo; } }
    }
}
