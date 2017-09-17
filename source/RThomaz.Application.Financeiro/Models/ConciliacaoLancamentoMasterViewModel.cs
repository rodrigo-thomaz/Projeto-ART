using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class ConciliacaoLancamentoMasterViewModel
    {
        private readonly Guid _lancamentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _historico;
        private readonly string _pessoaNome;
        private readonly string _valorPagamento;
        private readonly string _dataPagamento;
        private readonly string _valorConciliado;
        private readonly long? _transferenciaId;
        private readonly long? _programacaoId;

        public ConciliacaoLancamentoMasterViewModel
            (
                  Guid lancamentoId
                , TipoTransacao tipoTransacao
                , string historico
                , string pessoaNome
                , string valorPagamento
                , string dataPagamento
                , string valorConciliado
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
        public string ValorPagamento { get { return _valorPagamento; } }
        public string DataPagamento { get { return _dataPagamento; } }
        public string ValorConciliado { get { return _valorConciliado; } }
        public long? TransferenciaId { get { return _transferenciaId; } }
        public long? ProgramacaoId { get { return _programacaoId; } }
    }
}