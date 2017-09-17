using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class MovimentoDetailViewModel
    {
        public long MovimentoId { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public long ContaId { get; set; }

        public TipoConta TipoConta { get; set; }

        public DateTime DataMovimento { get; set; }

        public decimal ValorMovimento { get; set; }

        public string Historico { get; set; }        

        public string Observacao { get; set; }

        public MovimentoImportacaoDetailViewModel MovimentoImportacao { get; set; }

        public bool EstaConciliado { get; set; }
    }
}