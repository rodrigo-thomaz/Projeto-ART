using RThomaz.Database.Enums;
using System;

namespace RThomaz.Business.DTOs
{
    public class TransferenciaDetailDTO
    {
        private readonly long _transferenciaId;
        private readonly long _contaOrigemId;
        private readonly TipoConta _tipoContaOrigem;
        private readonly long _contaDestinoId;
        private readonly TipoConta _tipoContaDestino;
        private readonly string _historico;
        private readonly string _observacao;
        private readonly string _numero;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;

        public TransferenciaDetailDTO
            (
                  long transferenciaId
                , long contaOrigemId
                , TipoConta tipoContaOrigem
                , long contaDestinoId
                , TipoConta tipoContaDestino
                , string historico
                , string observacao
                , string numero
                , DateTime dataVencimento
                , decimal valorVencimento
            )
        {
            _transferenciaId = transferenciaId;
            _contaOrigemId = contaOrigemId;
            _tipoContaOrigem = tipoContaOrigem;
            _contaDestinoId = contaDestinoId;
            _tipoContaDestino = tipoContaDestino;
            _historico = historico;
            _observacao = observacao;
            _numero = numero;
            _dataVencimento = dataVencimento;
            _valorVencimento = valorVencimento;
        }

        public long TransferenciaId { get { return _transferenciaId; } }
        public long ContaOrigemId { get { return _contaOrigemId; } }
        public TipoConta TipoContaOrigem { get { return _tipoContaOrigem; } }
        public long ContaDestinoId { get { return _contaDestinoId; } }
        public TipoConta TipoContaDestino { get { return _tipoContaDestino; } }
        public string Historico { get { return _historico; } }
        public string Observacao { get { return _observacao; } }
        public string Numero { get { return _numero; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
    }
}