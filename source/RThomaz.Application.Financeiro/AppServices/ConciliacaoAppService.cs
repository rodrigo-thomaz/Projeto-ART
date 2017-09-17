using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class ConciliacaoAppService : AppServiceBase, IConciliacaoAppService
    {
        #region private fields

        private readonly IConciliacaoService _conciliacaoService;

        #endregion

        #region constructors

        public ConciliacaoAppService(IConciliacaoService conciliacaoService)
        {
            _conciliacaoService = conciliacaoService;
        }

        #endregion

        #region public voids

        public async Task<List<ConciliacaoLancamentoMasterViewModel>> GetLancamentosConciliados(long movimentoId, TipoTransacao tipoTransacao)
        {
            var masterListDTO = await _conciliacaoService.GetLancamentosConciliados(movimentoId, tipoTransacao);
            var selectListModel = ConvertDTOToModel(masterListDTO);
            return selectListModel;
        }

        public async Task<List<ConciliacaoMovimentoMasterViewModel>> GetMovimentosConciliados(long lancamentoId, TipoTransacao tipoTransacao)
        {
            var masterListDTO = await _conciliacaoService.GetMovimentosConciliados(lancamentoId, tipoTransacao);
            var selectListModel = ConvertDTOToModel(masterListDTO);
            return selectListModel;
        }

        #endregion

        #region private voids

        private List<ConciliacaoLancamentoMasterViewModel> ConvertDTOToModel(List<ConciliacaoLancamentoMasterViewDTO> dtos)
        {
            var result = new List<ConciliacaoLancamentoMasterViewModel>();

            foreach (var item in dtos)
            {
                var masterModel = new ConciliacaoLancamentoMasterViewModel(
                    lancamentoId: item.LancamentoId,
                    tipoTransacao: item.TipoTransacao,
                    historico: item.Historico,
                    pessoaNome: item.PessoaNome,
                    valorPagamento: item.ValorPagamento < 0 ? string.Format("({0})", item.ValorPagamento.ToString("N2")) : item.ValorPagamento.ToString("N2"),
                    dataPagamento: item.DataPagamento.ToShortDateString(),
                    valorConciliado: item.ValorConciliado < 0 ? string.Format("({0})", item.ValorConciliado.ToString("N2")) : item.ValorConciliado.ToString("N2"),
                    transferenciaId: item.TransferenciaId,
                    programacaoId: item.ProgramacaoId
                );

                result.Add(masterModel);
            }

            return result;
        }

        private List<ConciliacaoMovimentoMasterViewModel> ConvertDTOToModel(List<ConciliacaoMovimentoMasterViewDTO> dtos)
        {
            var result = new List<ConciliacaoMovimentoMasterViewModel>();

            foreach (var item in dtos)
            {
                var masterModel = new ConciliacaoMovimentoMasterViewModel(
                    movimentoId: item.MovimentoId,
                    tipoTransacao: item.TipoTransacao,
                    contaId: item.ContaId,
                    contaNome: item.ContaNome,
                    tipoConta: item.TipoConta,
                    historico: item.Historico,
                    valorMovimento: item.ValorMovimento < 0 ? string.Format("({0})", item.ValorMovimento.ToString("N2")) : item.ValorMovimento.ToString("N2"),
                    dataMovimento: item.DataMovimento.ToShortDateString(),
                    valorConciliado: item.ValorConciliado < 0 ? string.Format("({0})", item.ValorConciliado.ToString("N2")) : item.ValorConciliado.ToString("N2"),
                    estaConciliado: item.EstaConciliado
                );

                result.Add(masterModel);
            }

            return result;
        }

        #endregion
    }
}