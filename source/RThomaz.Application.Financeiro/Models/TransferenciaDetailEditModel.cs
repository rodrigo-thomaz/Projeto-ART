using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class TransferenciaDetailEditModel
    {
        public long TransferenciaId { get; set; }

        public long ContaOrigemId { get; set; }

        public TipoConta TipoContaOrigem { get; set; }

        public long ContaDestinoId { get; set; }

        public TipoConta TipoContaDestino { get; set; }

        public string Historico { get; set; }

        public string Numero { get; set; }

        public DateTime DataVencimento { get; set; }
        
        public decimal ValorVencimento { get; set; }

        public string Observacao { get; set; }
    }
}