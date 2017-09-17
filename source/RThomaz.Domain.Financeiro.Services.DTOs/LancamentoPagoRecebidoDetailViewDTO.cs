using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LancamentoPagoRecebidoDetailViewDTO
    {
        private readonly Guid _lancamentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly PessoaSelectViewDTO _pessoa;
        private readonly ContaSelectViewDTO _conta;
        private readonly List<RateioDetailViewDTO> _rateios;
        private readonly List<ConciliacaoDetailViewDTO> _conciliacoes;
        private readonly string _historico;
        private readonly string _observacao;
        private readonly string _numero;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;
        private readonly DateTime _dataPagamento;
        private readonly decimal _valorPagamento;

        public LancamentoPagoRecebidoDetailViewDTO
            (
                  Guid lancamentoId
                , TipoTransacao tipoTransacao
                , PessoaSelectViewDTO pessoa
                , ContaSelectViewDTO conta
                , List<RateioDetailViewDTO> rateios
                , List<ConciliacaoDetailViewDTO> conciliacoes
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
            _tipoTransacao = tipoTransacao;
            _pessoa = pessoa;
            _conta = conta;            
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

        public Guid LancamentoId { get { return _lancamentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public PessoaSelectViewDTO Pessoa { get { return _pessoa; } }
        public ContaSelectViewDTO Conta { get { return _conta; } }
        public List<RateioDetailViewDTO> Rateios { get { return _rateios; } }
        public List<ConciliacaoDetailViewDTO> Conciliacoes { get { return _conciliacoes; } }
        public string Historico { get { return _historico; } }
        public string Observacao { get { return _observacao; } }
        public string Numero { get { return _numero; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
        public DateTime DataPagamento { get { return _dataPagamento; } }
        public decimal ValorPagamento { get { return _valorPagamento; } }        
    }
}