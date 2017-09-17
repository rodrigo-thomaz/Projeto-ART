using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LancamentoPagoRecebidoMasterDTO
    {
        private readonly Guid _lancamentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _historico;
        private readonly string _pessoaNome;
        private readonly DateTime _dataPagamento;
        private readonly decimal _valorPagamento;
        private readonly long? _transferenciaId;
        private readonly long? _programacaoId;
        private readonly decimal _saldo;

        public LancamentoPagoRecebidoMasterDTO
            (
                  Guid lancamentoId
                , TipoTransacao tipoTransacao
                , string historico
                , string pessoaNome
                , DateTime dataPagamento
                , decimal valorPagamento
                , long? transferenciaId
                , long? programacaoId
                , decimal saldo
            )
        {
            _lancamentoId = lancamentoId;
            _tipoTransacao = tipoTransacao;
            _historico = historico;
            _pessoaNome = pessoaNome;
            _dataPagamento = dataPagamento;
            _valorPagamento = valorPagamento;
            _transferenciaId = transferenciaId;
            _programacaoId = programacaoId;
            _saldo = saldo;
        }

        public Guid LancamentoId { get { return _lancamentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string Historico { get { return _historico; } }
        public string PessoaNome { get { return _pessoaNome; } }
        public DateTime DataPagamento { get { return _dataPagamento; } }
        public decimal ValorPagamento { get { return _valorPagamento; } }
        public long? TransferenciaId { get { return _transferenciaId; } }
        public long? ProgramacaoId { get { return _programacaoId; } }
        public decimal Saldo { get { return _saldo; } }
    }
}