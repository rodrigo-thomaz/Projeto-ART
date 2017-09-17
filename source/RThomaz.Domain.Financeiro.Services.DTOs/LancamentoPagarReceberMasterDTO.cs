using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LancamentoPagarReceberMasterDTO
    {
        private readonly Guid _lancamentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _historico;
        private readonly string _pessoaNome;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;
        private readonly long? _transferenciaId;
        private readonly long? _programacaoId;
        private readonly decimal _saldo;

        public LancamentoPagarReceberMasterDTO
            (
                  Guid lancamentoId
                , TipoTransacao tipoTransacao
                , string historico
                , string pessoaNome
                , DateTime dataVencimento
                , decimal valorVencimento
                , long? transferenciaId
                , long? programacaoId
                , decimal saldo
            )
        {
            _lancamentoId = lancamentoId;
            _tipoTransacao = tipoTransacao;
            _historico = historico;
            _pessoaNome = pessoaNome;
            _dataVencimento = dataVencimento;
            _valorVencimento = valorVencimento;
            _transferenciaId = transferenciaId;
            _programacaoId = programacaoId;
            _saldo = saldo;
        }

        public Guid LancamentoId { get { return _lancamentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string Historico { get { return _historico; } }
        public string PessoaNome { get { return _pessoaNome; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
        public long? TransferenciaId { get { return _transferenciaId; } }
        public long? ProgramacaoId { get { return _programacaoId; } }
        public decimal Saldo { get { return _saldo; } }
    }
}