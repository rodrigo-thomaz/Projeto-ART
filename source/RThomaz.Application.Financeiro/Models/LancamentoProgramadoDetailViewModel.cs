using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;

namespace RThomaz.Application.Financeiro.Models
{
    public class LancamentoProgramadoDetailViewModel : ProgramacaoDetailViewBaseModel
    {
        private readonly TipoTransacao _tipoTransacao;
        private readonly List<RateioDetailViewModel> _rateios;
        private readonly PessoaSelectViewModel _pessoaSelectView;
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;

        public LancamentoProgramadoDetailViewModel
        (
            long programacaoId
            , TipoTransacao tipoTransacao
            , List<RateioDetailViewModel> rateios
            , PessoaSelectViewModel pessoaSelectView
            , long contaId
            , TipoConta tipoConta
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
        : base
        (
              programacaoId: programacaoId
            , dataInicial: dataInicial
            , dataFinal: dataFinal
            , frequencia: frequencia
            , dia: dia
            , hasDomingo: hasDomingo
            , hasSegunda: hasSegunda
            , hasTerca: hasTerca
            , hasQuarta: hasQuarta
            , hasQuinta: hasQuinta
            , hasSexta: hasSexta
            , hasSabado: hasSabado
            , historico: historico
            , valorVencimento: valorVencimento
            , observacao: observacao
        )
        {
            _tipoTransacao = tipoTransacao;
            _rateios = rateios;
            _pessoaSelectView = pessoaSelectView;
            _contaId = contaId;
            _tipoConta = tipoConta;
        }

        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }

        public List<RateioDetailViewModel> Rateios { get { return _rateios; } }

        public PessoaSelectViewModel PessoaSelectView { get { return _pessoaSelectView; } }

        public long ContaId { get { return _contaId; } }

        public TipoConta TipoConta { get { return _tipoConta; } }
    }
}