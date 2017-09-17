using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;

namespace RThomaz.Application.Financeiro.Models
{
    public class MovimentoImportacaoOFXModel
    {
        public long ContaId { get; set; }

        public TipoConta TipoConta { get; set; }

        public string ContaNome { get; set; }

        public string BancoNome { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public List<MovimentoImportacaoOFXItemModel> Movimentacoes { get; set; }
    }
}