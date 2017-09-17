using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class TransferenciaProgramadaDetailViewModel : ProgramacaoDetailViewBaseModel
    {
        private readonly ContaSelectViewModel _contaOrigem;
        private readonly ContaSelectViewModel _contaDestino;

        public TransferenciaProgramadaDetailViewModel
            (
                  long programacaoId
                , ContaSelectViewModel contaOrigem
                , ContaSelectViewModel contaDestino
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
            _contaOrigem = contaOrigem;
            _contaDestino = contaDestino;
        }

        public ContaSelectViewModel ContaOrigem { get { return _contaOrigem; } }
        public ContaSelectViewModel ContaDestino { get { return _contaDestino; } }
    }
}