using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoSelectViewDTO
    {
        private readonly long _movimentoId;
        private readonly string _historico;
        private readonly DateTime _dataMovimento;
        private readonly decimal _valorMovimento;
        private readonly decimal _valorDisponivel;

        public MovimentoSelectViewDTO(long movimentoId, string historico, DateTime dataMovimento, decimal valorMovimento, decimal valorDisponivel)
        {
            _movimentoId = movimentoId;
            _historico = historico;
            _dataMovimento = dataMovimento;
            _valorMovimento = valorMovimento;
            _valorDisponivel = valorDisponivel;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public string Historico { get { return _historico; } }
        public DateTime DataMovimento { get { return _dataMovimento; } }
        public decimal ValorMovimento { get { return _valorMovimento; } }
        public decimal ValorDisponivel { get { return _valorDisponivel; } }
    }
}
