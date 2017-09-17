namespace RThomaz.Application.Financeiro.Models
{
    public class CBOOcupacaoSelectViewModel
    {
        private readonly int _cboOcupacaoId;
        private readonly string _codigo;
        private readonly string _titulo;

        public CBOOcupacaoSelectViewModel
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

        public int CBOOcupacaoId { get { return _cboOcupacaoId; } }
        public string Codigo { get { return _codigo; } }
        public string Titulo { get { return _titulo; } }
    }
}
