using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TransferenciaDetailViewDTO
    {
        private readonly long _transferenciaId;
        private readonly ContaSelectViewDTO _contaOrigem;
        private readonly ContaSelectViewDTO _contaDestino;
        private readonly string _historico;
        private readonly string _observacao;
        private readonly string _numero;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;

        public TransferenciaDetailViewDTO
            (
                  long transferenciaId
                , ContaSelectViewDTO contaOrigem
                , ContaSelectViewDTO contaDestino
                , string historico
                , string observacao
                , string numero
                , DateTime dataVencimento
                , decimal valorVencimento
            )
        {
            _transferenciaId = transferenciaId;
            _contaOrigem = contaOrigem;
            _contaDestino = contaDestino;
            _historico = historico;
            _observacao = observacao;
            _numero = numero;
            _dataVencimento = dataVencimento;
            _valorVencimento = valorVencimento;
        }

        public long TransferenciaId { get { return _transferenciaId; } }
        public ContaSelectViewDTO ContaOrigem { get { return _contaOrigem; } }
        public ContaSelectViewDTO ContaDestino { get { return _contaDestino; } }
        public string Historico { get { return _historico; } }
        public string Observacao { get { return _observacao; } }
        public string Numero { get { return _numero; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
    }
}