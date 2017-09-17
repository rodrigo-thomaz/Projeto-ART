using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class TransferenciaDetailViewModel
    {
        private readonly long _transferenciaId;
        private readonly ContaSelectViewModel _contaOrigem;
        private readonly ContaSelectViewModel _contaDestino;
        private readonly string _historico;
        private readonly string _observacao;
        private readonly string _numero;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;

        public TransferenciaDetailViewModel
            (
                  long transferenciaId
                , ContaSelectViewModel contaOrigem
                , ContaSelectViewModel contaDestino
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
        public ContaSelectViewModel ContaOrigem { get { return _contaOrigem; } }
        public ContaSelectViewModel ContaDestino { get { return _contaDestino; } }
        public string Historico { get { return _historico; } }
        public string Observacao { get { return _observacao; } }
        public string Numero { get { return _numero; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
    }
}