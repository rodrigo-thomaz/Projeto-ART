using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;

namespace RThomaz.Application.Financeiro.Models
{
    public class LancamentoPagarReceberDetailInsertModel
    {
        public long LancamentoId { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public long? PessoaId { get; set; }

        public TipoPessoa? TipoPessoa { get; set; }

        public long ContaId { get; set; }

        public TipoConta TipoConta { get; set; }

        public string Historico { get; set; }

        public string Numero { get; set; }

        public DateTime DataVencimento { get; set; }

        public decimal ValorVencimento { get; set; }

        public bool EstaPago { get; set; }

        public DateTime? DataPagamento { get; set; }

        public decimal? ValorPagamento { get; set; }

        public List<RateioDetailUpdateModel> Rateios { get; set; }

        public List<ConciliacaoDetailUpdateModel> Conciliacoes { get; set; }

        public string Observacao { get; set; }
    }
}