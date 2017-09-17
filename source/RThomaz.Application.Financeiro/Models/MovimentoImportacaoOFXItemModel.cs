using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class MovimentoImportacaoOFXItemModel
    {
        public TipoTransacao TipoTransacao { get; set; }

        public DateTime DataMovimento { get; set; }

        public decimal ValorMovimento { get; set; }       

        public string Historico { get; set; }        

        public bool Existe { get; set; }
    }
}