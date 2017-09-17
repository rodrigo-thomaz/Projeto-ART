using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LancamentoProgramadoInsertDTO : ProgramacaoDetailBaseDTO
    {
        private readonly TipoTransacao _tipoTransacao;
        private readonly long? _pessoaId;
        private readonly TipoPessoa? _tipoPessoa;
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;
        private readonly List<RateioDetailUpdateDTO> _rateios;

        public LancamentoProgramadoInsertDTO
            (
                TipoTransacao tipoTransacao
                , long? pessoaId
                , TipoPessoa? tipoPessoa
                , long contaId
                , TipoConta tipoConta
                , List<RateioDetailUpdateDTO> rateios
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
            ): base
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
            if (valorVencimento == 0)
            {
                throw new ArgumentOutOfRangeException("valorVencimento", "Não permite zero");
            }

            _tipoTransacao = tipoTransacao;
            _pessoaId = pessoaId;
            _tipoPessoa = tipoPessoa;
            _contaId = contaId;
            _tipoConta = tipoConta;
            _rateios = rateios;

            if (tipoTransacao == TipoTransacao.Credito)
            {
                _valorVencimento = Math.Abs(valorVencimento);
            }
            else
            {
                _valorVencimento = Math.Abs(valorVencimento) * (-1);
            }    
        }

        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public long? PessoaId { get { return _pessoaId; } }
        public TipoPessoa? TipoPessoa { get { return _tipoPessoa; } }
        public long ContaId { get { return _contaId; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public List<RateioDetailUpdateDTO> Rateios { get { return _rateios; } }
    }
}
