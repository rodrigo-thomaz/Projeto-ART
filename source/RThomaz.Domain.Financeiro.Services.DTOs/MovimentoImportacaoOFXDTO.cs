using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoImportacaoOFXDTO
    {
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;
        private readonly string _contaNome;
        private readonly string _bancoNome;
        private readonly DateTime _dataInicio;
        private readonly DateTime _dataFim;
        private readonly List<MovimentoImportacaoOFXItemDTO> _movimentacoes;

        public MovimentoImportacaoOFXDTO
            (
                  long contaId
                , TipoConta tipoConta
                , string contaNome
                , string bancoNome
                , DateTime dataInicio
                , DateTime dataFim
                , List<MovimentoImportacaoOFXItemDTO> movimentacoes                
            )
        {
            _contaId = contaId;
            _tipoConta = tipoConta;
            _contaNome = contaNome;
            _bancoNome = bancoNome;
            _dataInicio = dataInicio;
            _dataFim = dataFim;
            _movimentacoes = movimentacoes;
        }

        public long ContaId { get { return _contaId; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public string ContaNome { get { return _contaNome; } }
        public string BancoNome { get { return _bancoNome; } }
        public DateTime DataInicio { get { return _dataInicio; } }
        public DateTime DataFim { get { return _dataFim; } }
        public List<MovimentoImportacaoOFXItemDTO> Movimentacoes { get { return _movimentacoes; } }        
    }
}