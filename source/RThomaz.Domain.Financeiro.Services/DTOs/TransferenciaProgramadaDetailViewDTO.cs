using RThomaz.Database.Enums;
using System;

namespace RThomaz.Business.DTOs
{
    public class TransferenciaProgramadaDetailDTO : ProgramacaoDetailBaseDTO
    {
        private readonly long _contaOrigemId;
        private readonly TipoConta _tipoContaOrigem;
        private readonly long _contaDestinoId;
        private readonly TipoConta _tipoContaDestino;

        public TransferenciaProgramadaDetailDTO
            (
                  long programacaoId
                , long contaOrigemId
                , TipoConta tipoContaOrigem
                , long contaDestinoId
                , TipoConta tipoContaDestino
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
            _contaOrigemId = contaOrigemId;
            _tipoContaOrigem = tipoContaOrigem;
            _contaDestinoId = contaDestinoId;
            _tipoContaDestino = tipoContaDestino;
        }

        public long ContaOrigemId { get { return _contaOrigemId; } }
        public TipoConta TipoContaOrigem { get { return _tipoContaOrigem; } }
        public long ContaDestinoId { get { return _contaDestinoId; } }
        public TipoConta TipoContaDestino { get { return _tipoContaDestino; } }
    }
}