using RThomaz.Database.Enums;
using System;

namespace RThomaz.Business.DTOs
{
    public class LancamentoPagarReceberInsertDTO
    {
        private readonly TipoTransacao _tipoTransacao;
        private readonly long? _pessoaId;
        private readonly TipoPessoa? _tipoPessoa;
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;
        private readonly bool _estaPago;
        private readonly DateTime? _dataPagamento;
        private readonly decimal? _valorPagamento;        
        private readonly RateioDetailDTO[] _rateios;
        private readonly ConciliacaoSaveDTO[] _conciliacoes;
        private readonly string _historico;
        private readonly string _numero;
        private readonly string _observacao;

        public LancamentoPagarReceberInsertDTO
            (
                  TipoTransacao tipoTransacao
                , long? pessoaId
                , TipoPessoa? tipoPessoa
                , long contaId
                , TipoConta tipoConta
                , DateTime dataVencimento
                , decimal valorVencimento
                , RateioDetailDTO[] rateios
                , ConciliacaoSaveDTO[] conciliacoes
                , string historico
                , string numero
                , string observacao
                , bool estaPago
                , DateTime? dataPagamento
                , decimal? valorPagamento                
            )
        {
            if (valorVencimento == 0)
            {
                throw new ArgumentOutOfRangeException("valorVencimento", "Não permite zero");
            }
            if (valorPagamento == 0)
            {
                throw new ArgumentOutOfRangeException("valorPagamento", "Não permite zero");
            }

            _tipoTransacao = tipoTransacao;
            _pessoaId = pessoaId;
            _tipoPessoa = tipoPessoa;
            _contaId = contaId;
            _tipoConta = tipoConta;
            _dataVencimento = dataVencimento;
            _rateios = rateios;
            _conciliacoes = conciliacoes;
            _historico = historico;
            _numero = numero;
            _observacao = observacao;
            _estaPago = estaPago;
            _dataPagamento = dataPagamento;

            if (tipoTransacao == TipoTransacao.Credito)
            {
                _valorVencimento = Math.Abs(valorVencimento);
                if (valorPagamento.HasValue)
                {
                    _valorPagamento = Math.Abs(valorPagamento.Value);
                }
            }
            else
            {
                _valorVencimento = Math.Abs(valorVencimento) * (-1);
                if (valorPagamento.HasValue)
                {
                    _valorPagamento = Math.Abs(valorPagamento.Value) * (-1);
                }
            }     
        }

        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public long? PessoaId { get { return _pessoaId; } }
        public TipoPessoa? TipoPessoa { get { return _tipoPessoa; } }
        public long ContaId { get { return _contaId; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
        public RateioDetailDTO[] Rateios { get { return _rateios; } }
        public ConciliacaoSaveDTO[] Conciliacoes { get { return _conciliacoes; } }
        public string Historico { get { return _historico; } }
        public string Numero { get { return _numero; } }
        public string Observacao { get { return _observacao; } }
        public bool EstaPago { get { return _estaPago; } }
        public DateTime? DataPagamento { get { return _dataPagamento; } }
        public decimal? ValorPagamento { get { return _valorPagamento; } }        
    }
}
