using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class LancamentoMasterModel
    {
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _tipoProgramacao;
        private readonly DateTime _dataVencimento;
        private readonly string _historico;
        private readonly string _pessoaNome;
        private readonly string _valorVencimento;
        private readonly string _saldo;
        private readonly Guid _lancamentoId;
        private readonly long? _transferenciaId;
        private readonly long? _programacaoId;

        public LancamentoMasterModel
            (
                  TipoTransacao tipoTransacao
                , string tipoProgramacao
                , DateTime dataVencimento
                , string historico
                , string pessoaNome
                , string valorVencimento
                , string saldo
                , Guid lancamentoId
                , long? transferenciaId
                , long? programacaoId
            )
        {
            _tipoTransacao = tipoTransacao;
            _tipoProgramacao = tipoProgramacao;
            _dataVencimento = dataVencimento;
            _historico = historico;
            _pessoaNome = pessoaNome;
            _valorVencimento = valorVencimento;
            _saldo = saldo;
            _lancamentoId = lancamentoId;
            _transferenciaId = transferenciaId;
            _programacaoId = programacaoId;
        }

        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string TipoProgramacao { get { return _tipoProgramacao; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public string Historico { get { return _historico; } }
        public string PessoaNome { get { return _pessoaNome; } }
        public string ValorVencimento { get { return _valorVencimento; } }
        public string Saldo { get { return _saldo; } }
        public Guid LancamentoId { get { return _lancamentoId; } }
        public long? TransferenciaId { get { return _transferenciaId; } }
        public long? ProgramacaoId { get { return _programacaoId; } }
    }
}