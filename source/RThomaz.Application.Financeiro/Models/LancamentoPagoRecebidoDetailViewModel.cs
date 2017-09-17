using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;

namespace RThomaz.Application.Financeiro.Models
{
    public class LancamentoPagoRecebidoDetailViewModel
    {
        public long LancamentoId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public PessoaSelectViewModel Pessoa { get; set; }
        public ContaSelectViewModel Conta { get; set; }
        public string Historico { get; set; }
        public string Numero { get; set; }
        public DateTime DataVencimento { get; set; }        
        public decimal ValorVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public List<RateioDetailViewModel> Rateios { get; set; }
        public List<ConciliacaoDetailViewModel> Conciliacoes { get; set; }
        public string Observacao { get; set; }
    }
}