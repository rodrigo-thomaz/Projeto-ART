using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public abstract class ProgramacaoDetailViewBaseDTO : ProgramacaoDetailBaseDTO
    {
        private readonly long _programacaoId;

        public ProgramacaoDetailViewBaseDTO
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
            : base
                (
                    dataInicial: dataInicial
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
            _programacaoId = programacaoId;
        }

        public long ProgramacaoId { get { return _programacaoId; } }
    }
}