using System;

namespace RThomaz.Business.DTOs
{
    public class ConciliacaoDetailDTO
    {
        private readonly long _movimentoId;
        private readonly string _historico;
        private readonly decimal _valorMovimento;
        private readonly DateTime _dataMovimento;
        private readonly decimal _valorConciliado;

        public ConciliacaoDetailDTO
            (
                  long movimentoId
                , string historico
                , decimal valorMovimento
                , DateTime dataMovimento
                , decimal valorConciliado
            )
        {
            _movimentoId = movimentoId;
            _historico = historico;
            _valorMovimento = valorMovimento;
            _dataMovimento = dataMovimento;
            _valorConciliado = valorConciliado;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public string Historico { get { return _historico; } }
        public decimal ValorMovimento { get { return _valorMovimento; } }
        public DateTime DataMovimento { get { return _dataMovimento; } }
        public decimal ValorConciliado { get { return _valorConciliado; } }
    }
}
