namespace RThomaz.Application.Financeiro.Models
{
    public class MovimentoSelectViewModel
    {
        public long MovimentoId { get; set; }
        public string Historico { get; set; }
        public string DataMovimento { get; set; }
        public string ValorMovimento { get; set; }
        public string ValorDisponivel { get; set; }  
    }
}