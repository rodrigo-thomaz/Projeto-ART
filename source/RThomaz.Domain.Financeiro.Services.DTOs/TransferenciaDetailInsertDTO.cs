using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TransferenciaDetailInsertDTO
    {        
        private readonly long _contaOrigemId;
        private readonly TipoConta _tipoContaOrigem;
        private readonly long _contaDestinoId;
        private readonly TipoConta _tipoContaDestino;
        private readonly string _historico;
        private readonly DateTime _dataVencimento;
        private readonly decimal _valorVencimento;
        private readonly string _numero;
        private readonly string _observacao;

        public TransferenciaDetailInsertDTO
            (                  
                  long contaOrigemId
                , TipoConta tipoContaOrigem
                , long contaDestinoId
                , TipoConta tipoContaDestino
                , string historico
                , DateTime dataVencimento
                , decimal valorVencimento
                , string numero
                , string observacao
            )
        {            
            _contaOrigemId = contaOrigemId;
            _tipoContaOrigem = tipoContaOrigem;
            _contaDestinoId = contaDestinoId;
            _tipoContaDestino = tipoContaDestino;
            _historico = historico;
            _dataVencimento = dataVencimento;
            _valorVencimento = valorVencimento;
            _numero = numero;
            _observacao = observacao;
        }

        public long ContaOrigemId { get { return _contaOrigemId; } }
        public TipoConta TipoContaOrigem { get { return _tipoContaOrigem; } }
        public long ContaDestinoId { get { return _contaDestinoId; } }
        public TipoConta TipoContaDestino { get { return _tipoContaDestino; } }
        public string Historico { get { return _historico; } }
        public DateTime DataVencimento { get { return _dataVencimento; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
        public string Numero { get { return _numero; } }
        public string Observacao { get { return _observacao; } }
    }
}
