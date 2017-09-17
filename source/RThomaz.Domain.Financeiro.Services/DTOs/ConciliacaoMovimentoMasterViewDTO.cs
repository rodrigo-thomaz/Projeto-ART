using RThomaz.Database.Enums;
using System;

namespace RThomaz.Business.DTOs
{
    public class ConciliacaoMovimentoMasterDTO
    {
        private readonly long _movimentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly long _contaId;
        private readonly string _contaNome;
        private readonly TipoConta _tipoConta;
        private readonly string _historico;
        private readonly decimal _valorMovimento;
        private readonly DateTime _dataMovimento;
        private readonly decimal _valorConciliado;
        private readonly bool _estaConciliado;

        public ConciliacaoMovimentoMasterDTO
            (
                  long movimentoId
                , TipoTransacao tipoTransacao
                , long contaId
                , string contaNome
                , TipoConta tipoConta
                , string historico
                , decimal valorMovimento
                , DateTime dataMovimento
                , decimal valorConciliado   
                , bool estaConciliado
            )
        {
            _movimentoId = movimentoId;
            _tipoTransacao = tipoTransacao;
            _contaId = contaId;
            _contaNome = contaNome;
            _tipoConta = tipoConta;
            _historico = historico;
            _valorMovimento = valorMovimento;
            _dataMovimento = dataMovimento;
            _valorConciliado = valorConciliado;
            _estaConciliado = estaConciliado;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public long ContaId { get { return _contaId; } }
        public string ContaNome { get { return _contaNome; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public string Historico { get { return _historico; } }
        public decimal ValorMovimento { get { return _valorMovimento; } }
        public DateTime DataMovimento { get { return _dataMovimento; } }
        public decimal ValorConciliado { get { return _valorConciliado; } }
        public bool EstaConciliado { get { return _estaConciliado; } }
    }
}