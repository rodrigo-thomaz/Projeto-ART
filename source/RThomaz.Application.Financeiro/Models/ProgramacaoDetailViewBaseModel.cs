using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public abstract class ProgramacaoDetailViewBaseModel
    {
        private readonly long _programacaoId;
        private DateTime _dataInicial;
        private DateTime _dataFinal;
        private Frequencia _frequencia;
        private byte? _dia;
        private bool? _hasDomingo;
        private bool? _hasSegunda;
        private bool? _hasTerca;
        private bool? _hasQuarta;
        private bool? _hasQuinta;
        private bool? _hasSexta;
        private bool? _hasSabado;
        private string _historico;
        protected decimal _valorVencimento;
        private string _observacao;

        public ProgramacaoDetailViewBaseModel
            (
                  long programacaoId
                , DateTime dataInicial
                , DateTime dataFinal
                , Frequencia frequencia
                , byte? dia
                , bool? hasDomingo
                , bool? hasSegunda
                , bool? hasTerca
                , bool? hasQuarta
                , bool? hasQuinta
                , bool? hasSexta
                , bool? hasSabado
                , string historico
                , decimal valorVencimento
                , string observacao
            )
        {
            _programacaoId = programacaoId;
            _dataInicial = dataInicial;
            _dataFinal = dataFinal;
            _frequencia = frequencia;
            _dia = dia;
            _hasDomingo = hasDomingo;
            _hasSegunda = hasSegunda;
            _hasTerca = hasTerca;
            _hasQuarta = hasQuarta;
            _hasQuinta = hasQuinta;
            _hasSexta = hasSexta;
            _hasSabado = hasSabado;
            _historico = historico;
            _valorVencimento = valorVencimento;
            _observacao = observacao;
        }

        public long ProgramacaoId { get { return _programacaoId; } }
        public DateTime DataInicial { get { return _dataInicial; } }
        public DateTime DataFinal { get { return _dataFinal; } }
        public Frequencia Frequencia { get { return _frequencia; } }
        public byte? Dia { get { return _dia; } }
        public bool? HasDomingo { get { return _hasDomingo; } }
        public bool? HasSegunda { get { return _hasSegunda; } }
        public bool? HasTerca { get { return _hasTerca; } }
        public bool? HasQuarta { get { return _hasQuarta; } }
        public bool? HasQuinta { get { return _hasQuinta; } }
        public bool? HasSexta { get { return _hasSexta; } }
        public bool? HasSabado { get { return _hasSabado; } }
        public string Historico { get { return _historico; } }
        public decimal ValorVencimento { get { return _valorVencimento; } }
        public string Observacao { get { return _observacao; } }           
    }
}