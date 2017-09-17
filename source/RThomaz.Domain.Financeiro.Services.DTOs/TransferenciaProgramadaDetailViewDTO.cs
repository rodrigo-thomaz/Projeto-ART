using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class TransferenciaProgramadaDetailViewDTO : ProgramacaoDetailViewBaseDTO
    {
        private readonly ContaSelectViewDTO _contaOrigem;
        private readonly ContaSelectViewDTO _contaDestino;

        public TransferenciaProgramadaDetailViewDTO
            (
                  long programacaoId
                , ContaSelectViewDTO contaOrigem
                , ContaSelectViewDTO contaDestino
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

        public ContaSelectViewDTO ContaOrigem { get { return _contaOrigem; } }
        public ContaSelectViewDTO ContaDestino { get { return _contaDestino; } }
    }
}