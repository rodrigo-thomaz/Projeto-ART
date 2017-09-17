using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class ConciliacaoDetailUpdateModel
    {
        public long MovimentoId { get; set; }
        public string Historico { get; set; }
        public decimal ValorConciliado { get; set; }
        public decimal ValorMovimento { get; set; }
        public DateTime DataMovimento { get; set; }
    }
}