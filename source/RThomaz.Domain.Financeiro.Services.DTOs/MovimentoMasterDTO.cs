using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoMasterDTO
    {
        private readonly long _movimentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly DateTime _dataMovimento;
        private readonly decimal _valorMovimento;
        private readonly string _historico;
        private readonly decimal? _saldo;
        private readonly decimal _totalConciliado;
        private readonly bool _estaConciliado;
        private readonly long? _movimentoImportacaoId;

        public MovimentoMasterDTO
            (
                  long movimentoId
                , TipoTransacao tipoTransacao
                , TipoConta tipoConta
                , DateTime dataMovimento
                , decimal valorMovimento
                , string historico
                , decimal? saldo
                , decimal totalConciliado
                , bool estaConciliado
                , long? movimentoImportacaoId
            )
        {
            _movimentoId = movimentoId;
            _tipoTransacao = tipoTransacao;
            _dataMovimento = dataMovimento;
            _valorMovimento = valorMovimento;
            _historico = historico;
            _saldo = saldo;
            _totalConciliado = totalConciliado;
            _estaConciliado = estaConciliado;
            _movimentoImportacaoId = movimentoImportacaoId;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public DateTime DataMovimento { get { return _dataMovimento; } }
        public decimal ValorMovimento { get { return _valorMovimento; } }
        public string Historico { get { return _historico; } }
        public decimal? Saldo { get { return _saldo; } }
        public decimal TotalConciliado { get { return _totalConciliado; } }
        public bool EstaConciliado { get { return _estaConciliado; } }
        public long? MovimentoImportacaoId { get { return _movimentoImportacaoId; } }
    }
}