using RThomaz.Database.Enums;
using System;

namespace RThomaz.Business.DTOs
{
    public class LancamentoPagoRecebidoDetailDTO
    {
        private readonly long _lancamentoId;
        private readonly string _pessoaNome;
        private readonly string _contaNome;
        private readonly TipoConta _tipoConta;
        private readonly TipoTransacao _tipoTransacao;
        private readonly RateioViewDTO[] _rateios;
        private readonly ConciliacaoDetailDTO[] _conciliacoes;
        private readonly string _historico;
        private readonly string _observacao;
        private readonly string _numero;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;
        private readonly DateTime _dataPagamento;
        private readonly decimal _valorPagamento;

        public LancamentoPagoRecebidoDetailDTO
            (
                  long lancamentoId
                , string pessoaNome
                , string contaNome
                , TipoConta tipoConta
                , TipoTransacao tipoTransacao
                , RateioViewDTO[] rateios
                , ConciliacaoDetailDTO[] conciliacoes
                , string historico
                , string observacao
                , string numero
                , DateTime dataVencimento
                , decimal valorVencimento
                , DateTime dataPagamento
                , decimal valorPagamento                
            )
        {
            _lancamentoId = lancamentoId;
            _pessoaNome = pessoaNome;
            _contaNome = contaNome;
            _tipoConta = tipoConta;
            _tipoTransacao = tipoTransacao;
            _rateios = rateios;
            _conciliacoes = conciliacoes;
            _historico = historico;
            _observacao = observacao;
            _numero = numero;
            _dataVencimento = dataVencimento;
            _valorVencimento = valorVencimento;
            _dataPagamento = dataPagamento;
            _valorPagamento = valorPagamento;            
        }

        public long LancamentoId { get { return _lancamentoId; } }
        public string PessoaNome { get { return _pessoaNome; } }
        public string ContaNome { get { return _contaNome; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public RateioViewDTO[] Rateios { get { return _rateios; } }
        public ConciliacaoDetailDTO[] Conciliacoes { get { return _conciliacoes; } }
        public string Historico { get { return _historico; } }
        public string Observacao { get { return _observacao; } }
        public string Numero { get { return _numero; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
        public DateTime DataPagamento { get { return _dataPagamento; } }
        public decimal ValorPagamento { get { return _valorPagamento; } }        
    }
}