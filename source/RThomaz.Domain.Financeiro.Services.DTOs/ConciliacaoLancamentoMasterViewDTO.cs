using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ConciliacaoLancamentoMasterViewDTO
    {
        private readonly Guid _lancamentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _historico;
        private readonly string _pessoaNome;
        private readonly decimal _valorPagamento;
        private readonly DateTime _dataPagamento;
        private readonly decimal _valorConciliado;
        private readonly long? _transferenciaId;
        private readonly long? _programacaoId;

        public ConciliacaoLancamentoMasterViewDTO
            (
                  Guid lancamentoId
                , TipoTransacao tipoTransacao
                , string historico
                , string pessoaNome
                , decimal valorPagamento
                , DateTime dataPagamento
                , decimal valorConciliado
                , long? transferenciaId
                , long? programacaoId
            )
        {
            _lancamentoId = lancamentoId;
            _tipoTransacao = tipoTransacao;
            _historico = historico;
            _pessoaNome = pessoaNome;
            _valorPagamento = valorPagamento;
            _dataPagamento = dataPagamento;
            _valorConciliado = valorConciliado;
            _transferenciaId = transferenciaId;
            _programacaoId = programacaoId;
        }

        public Guid LancamentoId { get { return _lancamentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string Historico { get { return _historico; } }
        public string PessoaNome { get { return _pessoaNome; } }
        public decimal ValorPagamento { get { return _valorPagamento; } }
        public DateTime DataPagamento { get { return _dataPagamento; } }
        public decimal ValorConciliado { get { return _valorConciliado; } }
        public long? TransferenciaId { get { return _transferenciaId; } }
        public long? ProgramacaoId { get { return _programacaoId; } }
    }
}