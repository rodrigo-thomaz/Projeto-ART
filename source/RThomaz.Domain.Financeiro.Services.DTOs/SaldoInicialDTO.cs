using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class SaldoInicialDTO
    {
        private readonly DateTime _data;
        private readonly decimal _valor;

        public SaldoInicialDTO
            (
                  DateTime data
                , decimal valor
            )
        {
            _data = data;
            _valor = valor;
        }

        public DateTime Data { get { return _data; } }
        public decimal Valor { get { return _valor; } }
    }
}
