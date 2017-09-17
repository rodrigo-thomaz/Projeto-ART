using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.Helpers.Ofx
{
    public class OfxTransaction
    {
        public TipoTransacao TipoTransacao;
        public string DatePosted;
        public string TransAmount;
        public string FITID;
        public string Name;
        public string Memo;
        public string CheckNum;
    }
}
