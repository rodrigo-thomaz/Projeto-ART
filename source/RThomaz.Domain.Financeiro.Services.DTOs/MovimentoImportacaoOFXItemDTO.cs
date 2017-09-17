using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoImportacaoOFXItemDTO
    {
        private readonly TipoTransacao _tipoTransacao;
        private readonly DateTime _dataMovimento;
        private readonly decimal _valorMovimento;
        private readonly string _historico;
        private readonly bool _existe;

        public MovimentoImportacaoOFXItemDTO
            (
                  TipoTransacao tipoTransacao
                , DateTime dataMovimento
                , decimal valorMovimento
                , string historico
                , bool existe
            )
        {
            _tipoTransacao = tipoTransacao;
            _dataMovimento = dataMovimento;
            _valorMovimento = valorMovimento;
            _historico = historico;
            _existe = existe;
        }

        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public DateTime DataMovimento { get { return _dataMovimento; } }
        public decimal ValorMovimento { get { return _valorMovimento; } }
        public string Historico { get { return _historico; } }
        public bool Existe { get { return _existe; } }    
    }
}