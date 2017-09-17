using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ProgramacaoMasterDTO
    {
        private readonly long _programacaoId;
        private readonly Frequencia _frequencia;
        private readonly DateTime _dataInicial;
        private readonly DateTime _dataFinal;
        private readonly string _historico;
        private readonly string _observacao;
        private readonly decimal _valorVencimento;

        private readonly TipoTransacao? _tipoTransacao;
        private readonly string _pessoaNome;
        private readonly string _contaNome;
                
        public ProgramacaoMasterDTO
            (
                  long programacaoId
                , Frequencia frequencia
                , DateTime dataInicial
                , DateTime dataFinal
                , string historico
                , string observacao
                , decimal valorVencimento
                , TipoTransacao? tipoTransacao
                , string pessoaNome
                , string contaNome
            )
        {
            _programacaoId = programacaoId;
            _frequencia = frequencia;
            _dataInicial = dataInicial;
            _dataFinal = dataFinal;
            _historico = historico;
            _observacao = observacao;
            _valorVencimento = valorVencimento;

            _tipoTransacao = tipoTransacao;
            _pessoaNome = pessoaNome;
            _contaNome = contaNome;
        }        
       
        public long ProgramacaoId { get { return _programacaoId; } }
        public Frequencia Frequencia { get { return _frequencia; } }
        public DateTime DataInicial { get { return _dataInicial; } }
        public DateTime DataFinal { get { return _dataFinal; } }
        public string Historico { get { return _historico; } }
        public string Observacao { get { return _observacao; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
        public TipoTransacao? TipoTransacao { get { return _tipoTransacao; } }
        public string PessoaNome { get { return _pessoaNome; } }
        public string ContaNome { get { return _contaNome; } }
    }
}
