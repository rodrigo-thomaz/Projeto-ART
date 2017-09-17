using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoImportacaoMasterDTO
    {
        private readonly long _movimentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly DateTime _dataMovimento;
        private readonly decimal _valorMovimento;
        private readonly string _historico;
        private readonly short _percentualConciliado;

        public MovimentoImportacaoMasterDTO
            (
                  long movimentoId
                , TipoTransacao tipoTransacao
                , DateTime dataMovimento
                , decimal valorMovimento
                , string historico
                , short percentualConciliado
            )
        {
            _movimentoId = movimentoId;
            _tipoTransacao = tipoTransacao;
            _dataMovimento = dataMovimento;
            _valorMovimento = valorMovimento;
            _historico = historico;
            _percentualConciliado = percentualConciliado;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public DateTime DataMovimento { get { return _dataMovimento; } }
        public decimal ValorMovimento { get { return _valorMovimento; } }
        public string Historico { get { return _historico; } }
        public short PercentualConciliado { get { return _percentualConciliado; } }
    }
}