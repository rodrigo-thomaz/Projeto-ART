namespace RThomaz.Application.Financeiro.Models
{
    public class MesAnoModel
    {
        public int Mes { get; set; }

        public int Ano { get; set; }

        public string Id
        {
            get
            {
                return string.Format("{0}{1}", Mes.ToString("00"), Ano.ToString());
            }
        }

        public string Text
        {
            get
            {
                return string.Format("{0}-{1}", Mes.ToString("00"), Ano.ToString());
            }
        }
    }
}