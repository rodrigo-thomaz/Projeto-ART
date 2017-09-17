namespace RThomaz.Business.DTOs
{
    public class CBOOcupacaoMasterDTO
    {
        private readonly int _cboOcupacaoId;
        private readonly string _codigo;
        private readonly string _titulo;

        public CBOOcupacaoMasterDTO
            (
                  int cboOcupacaoId
                , string codigo
                , string titulo               
            )
        {
            _cboOcupacaoId = cboOcupacaoId;
            _codigo = codigo;
            _titulo = titulo;
        }

        public long CBOOcupacaoId { get { return _cboOcupacaoId; } }
        public string Codigo { get { return _codigo; } }
        public string Titulo { get { return _titulo; } }
    }
}
