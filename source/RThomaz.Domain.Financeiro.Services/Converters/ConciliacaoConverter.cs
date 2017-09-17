using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.Converters
{
    public static class ConciliacaoConverter
    {
        public static List<ConciliacaoLancamentoMasterViewDTO> ConvertEntityToLancamentoDTO(ICollection<Conciliacao> conciliacoes)
        {
            var result = new List<ConciliacaoLancamentoMasterViewDTO>();

            foreach (var item in conciliacoes)
            {
                var nome = string.Empty;

                if (item.Pagamento.Lancamento.PessoaId.HasValue)
                {
                    var pessoa = item.Pagamento.Lancamento.Pessoa;

                    if (pessoa is PessoaFisica)
                    {
                        var pessoaFisica = ((PessoaFisica)pessoa);
                        nome = pessoaFisica.NomeCompleto;
                    }
                    else if (pessoa is PessoaJuridica)
                    {
                        var pessoaJuridica = ((PessoaJuridica)pessoa);
                        nome = pessoaJuridica.NomeFantasia;
                    }
                }

                result.Add(new ConciliacaoLancamentoMasterViewDTO
                (
                    lancamentoId: item.LancamentoId,
                    tipoTransacao: item.TipoTransacao,
                    valorPagamento: item.Pagamento.ValorPagamento,
                    dataPagamento: item.Pagamento.DataPagamento,
                    valorConciliado: item.ValorConciliado,
                    historico: item.Pagamento.Lancamento.Historico,
                    pessoaNome: nome,
                    transferenciaId: item.Pagamento.Lancamento.TransferenciaId,
                    programacaoId: item.Pagamento.Lancamento.ProgramacaoId
                ));
            }

            return result;
        }        

        public static List<ConciliacaoMovimentoMasterViewDTO> ConvertEntityToMovimentoDTO(ICollection<Conciliacao> conciliacoes)
        {
            var result = new List<ConciliacaoMovimentoMasterViewDTO>();

            foreach (var item in conciliacoes)
            {
                string contaNome;

                if (item.Movimento.TipoConta == TipoConta.ContaCorrente)
                {                    
                    var banco = ((ContaCorrente)item.Movimento.Conta).Banco;
                    contaNome = ContaService.GetContaNome(new List<Banco> { banco }, item.Movimento.Conta);
                }
                else if (item.Movimento.TipoConta == TipoConta.ContaPoupanca)
                {                    
                    var banco = ((ContaPoupanca)item.Movimento.Conta).Banco;
                    contaNome = ContaService.GetContaNome(new List<Banco> { banco }, item.Movimento.Conta);
                }
                else
                {
                    contaNome = ContaService.GetContaNome(new List<Banco> { }, item.Movimento.Conta);
                }               

                result.Add(new ConciliacaoMovimentoMasterViewDTO
                (
                    movimentoId: item.MovimentoId,
                    tipoTransacao: item.TipoTransacao,
                    contaId: item.Movimento.ContaId,
                    contaNome: contaNome,
                    tipoConta: item.Movimento.TipoConta,
                    historico: item.Movimento.Historico,
                    valorMovimento: item.Movimento.ValorMovimento,
                    dataMovimento: item.Movimento.DataMovimento,
                    valorConciliado: item.ValorConciliado,
                    estaConciliado: item.Movimento.EstaConciliado
                ));
            }

            return result;
        }

        public static List<ConciliacaoDetailViewDTO> ConvertEntityToDTO(ICollection<Conciliacao> entities)
        {
            var result = new List<ConciliacaoDetailViewDTO>();
            foreach (var entity in entities)
            {
                result.Add(ConvertEntityToDTO(entity));
            }
            return result;
        }

        public static ConciliacaoDetailViewDTO ConvertEntityToDTO(Conciliacao entity)
        {
            return new ConciliacaoDetailViewDTO
            (
                movimentoId: entity.MovimentoId,
                historico: entity.Movimento.Historico,
                valorMovimento: entity.Movimento.ValorMovimento,
                dataMovimento: entity.Movimento.DataMovimento,
                valorConciliado: entity.ValorConciliado
            );
        }
    }
}
