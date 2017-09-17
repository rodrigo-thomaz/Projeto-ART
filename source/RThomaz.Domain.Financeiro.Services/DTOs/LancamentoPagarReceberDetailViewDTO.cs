using RThomaz.Database.Enums;
using System;

namespace RThomaz.Business.DTOs
{
    public class LancamentoPagarReceberDetailDTO
    {
        private readonly long _lancamentoId;
        private readonly long? _pessoaId;
        private readonly TipoPessoa? _tipoPessoa;
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;
        private readonly TipoTransacao _tipoTransacao;
        private readonly RateioDetailDTO[] _rateios;
        private readonly string _historico;
        private readonly string _observacao;
        private readonly string _numero;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;

        public LancamentoPagarReceberDetailDTO
            (
                  long lancamentoId
                , long? pessoaId
                , TipoPessoa? tipoPessoa
                , long contaId
                , TipoConta tipoConta
                , TipoTransacao tipoTransacao
                , RateioDetailDTO[] rateios
                , string historico
                , string observacao
                , string numero
                , DateTime dataVencimento
                , decimal valorVencimento           
            )
        {
            _lancamentoId = lancamentoId;
            _pessoaId = pessoaId;
            _tipoPessoa = tipoPessoa;
            _contaId = contaId;
            _tipoConta = tipoConta;
            _tipoTransacao = tipoTransacao;
            _rateios = rateios;
            _historico = historico;
            _observacao = observacao;
            _numero = numero;
            _dataVencimento = dataVencimento;
            _valorVencimento = valorVencimento;         
        }

        public long LancamentoId { get { return _lancamentoId; } }
        public long? PessoaId { get { return _pessoaId; } }
        public TipoPessoa? TipoPessoa { get { return _tipoPessoa; } }
        public long ContaId { get { return _contaId; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public RateioDetailDTO[] Rateios { get { return _rateios; } }
        public string Historico { get { return _historico; } }
        public string Observacao { get { return _observacao; } }
        public string Numero { get { return _numero; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }     
    }
}