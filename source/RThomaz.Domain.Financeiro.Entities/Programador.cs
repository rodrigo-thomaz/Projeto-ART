using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Programador
    {
        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public Frequencia Frequencia { get; set; }

        public byte? Dia { get; set; }

        public bool? HasDomingo { get; set; }

        public bool? HasSegunda { get; set; }

        public bool? HasTerca { get; set; }

        public bool? HasQuarta { get; set; }

        public bool? HasQuinta { get; set; }

        public bool? HasSexta { get; set; }

        public bool? HasSabado { get; set; }

        public string Historico { get; set; }

        public decimal ValorVencimento { get; set; }

        public string Observacao { get; set; }        
    }
}
