using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoManualEditDTO
    {
        private readonly long _movimentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;
        private readonly DateTime _dataMovimento;
        private readonly decimal _valorMovimento;
        private readonly string _historico;
        private readonly string _observacao;

        public MovimentoManualEditDTO
            (
                  long movimentoId
                , TipoTransacao tipoTransacao                
                , long contaId
                , TipoConta tipoConta
                , DateTime dataMovimento
                , decimal valorMovimento
                , string historico
                , string observacao                
            )
        {
            _movimentoId = movimentoId;
            _tipoTransacao = tipoTransacao;
            _contaId = contaId;
            _tipoConta = tipoConta;
            _dataMovimento = dataMovimento;
            _valorMovimento = valorMovimento;
            _historico = historico;
            _observacao = observacao;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public long ContaId { get { return _contaId; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public DateTime DataMovimento { get { return _dataMovimento; } }
        public decimal ValorMovimento { get { return _valorMovimento; } }
        public string Historico { get { return _historico; } }
        public string Observacao { get { return _observacao; } }        
    }
}