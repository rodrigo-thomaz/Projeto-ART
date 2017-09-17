namespace RThomaz.Application.Financeiro.Models
{
    public class RateioDetailUpdateModel
    {
        public long PlanoContaId { get; set; }

        public long CentroCustoId { get; set; }

        public string Observacao { get; set; }

        public decimal Porcentagem { get; set; }
    }
}