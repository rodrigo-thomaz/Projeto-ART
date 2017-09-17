using AutoMapper;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Enums;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class MovimentoAppService : AppServiceBase, IMovimentoAppService
    {
        #region private fields

        private readonly IMovimentoService _movimentoService;

        #endregion

        #region constructors

        public MovimentoAppService(IMovimentoService movimentoService)
        {
            _movimentoService = movimentoService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<MovimentoMasterModel>>> GetMasterList(jQueryDataTableParameter param, MesAnoModel periodo, long contaId, TipoConta tipoConta, TipoTransacao? tipoTransacao, ConciliacaoStatus? conciliacaoStatus, ConciliacaoOrigem? conciliacaoOrigem)
        {
            var pagedListRequest = jQueryDataTableHelper<MovimentoMasterDTO>.ConvertToPagedListRequest(param);

            var periodoDTO = Mapper.Map<MesAnoModel, MesAnoDTO>(periodo);

            var masterContract = await _movimentoService.GetMasterList(
                pagedListRequest,
                periodoDTO,
                contaId,
                tipoConta,
                tipoTransacao,
                conciliacaoStatus,
                conciliacaoOrigem);

            var listOfMasterModel = ConvertToListOfMasterModel(masterContract.PagedListResponse.Data);

            string saldoAnteriorFormatted = string.Empty;

            if (masterContract.SaldoAnterior.HasValue)
            {
                saldoAnteriorFormatted = masterContract.SaldoAnterior.Value < 0
                    ? string.Format("({0})", masterContract.SaldoAnterior.Value.ToString("N2"))
                    : masterContract.SaldoAnterior.Value.ToString("N2");
            }

            var result = new jQueryDataTableResult<List<MovimentoMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.PagedListResponse.TotalRecords,
                totalDisplayRecords: masterContract.PagedListResponse.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<SelectListResponseModel<MovimentoSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest, TipoTransacao tipoTransacao)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var selectListDTOResponse = await _movimentoService.GetSelectViewList(selectListRequestDTO, tipoTransacao);
            var selectListModel = Mapper.Map<List<MovimentoSelectViewDTO>, List<MovimentoSelectViewModel>>(selectListDTOResponse.Data);
            var selectListModelResponse = new SelectListResponseModel<MovimentoSelectViewModel>(selectListDTOResponse.TotalRecords, selectListModel);
            return selectListModelResponse;
        }

        public async Task<MovimentoDetailViewModel> GetDetail(long movimentoId)
        {
            var dto = await _movimentoService.GetDetail(movimentoId);
            var result = Mapper.Map<MovimentoDetailViewDTO, MovimentoDetailViewModel>(dto);
            return result;
        }

        public async Task<List<MesAnoModel>> GetPeriodos(long contaId, TipoConta tipoConta)
        {
            var dtos = await _movimentoService.GetPeriodos(contaId, tipoConta);
            var result = Mapper.Map<List<MesAnoDTO>, List<MesAnoModel>>(dtos);
            return result;
        }

        public async Task<MovimentoDetailViewModel> Insert(MovimentoDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<MovimentoDetailInsertModel, MovimentoDetailInsertDTO>(model);
            var viewDTO = await _movimentoService.Insert(insertDTO);
            var viewModel = Mapper.Map<MovimentoDetailViewDTO, MovimentoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<MovimentoDetailViewModel> EditManual(MovimentoManualEditModel model)
        {
            var editDTO = Mapper.Map<MovimentoManualEditModel, MovimentoManualEditDTO>(model);
            var viewDTO = await _movimentoService.EditManual(editDTO);
            var viewModel = Mapper.Map<MovimentoDetailViewDTO, MovimentoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<MovimentoDetailViewModel> EditImportado(MovimentoImportadoEditModel model)
        {
            var editDTO = Mapper.Map<MovimentoImportadoEditModel, MovimentoImportadoEditDTO>(model);
            var viewDTO = await _movimentoService.EditImportado(editDTO);
            var viewModel = Mapper.Map<MovimentoDetailViewDTO, MovimentoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<MovimentoDetailViewModel> EditConciliado(MovimentoConciliadoEditModel model)
        {
            var editDTO = Mapper.Map<MovimentoConciliadoEditModel, MovimentoConciliadoEditDTO>(model);
            var viewDTO = await _movimentoService.EditConciliado(editDTO);
            var viewModel = Mapper.Map<MovimentoDetailViewDTO, MovimentoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long movimentoId)
        {
            var result = await _movimentoService.Remove(movimentoId);
            return result;
        }

        #endregion

        #region private voids

        private List<MovimentoMasterModel> ConvertToListOfMasterModel(List<MovimentoMasterDTO> collection)
        {
            var result = new List<MovimentoMasterModel>();

            foreach (var item in collection)
            {
                string saldoFormatted = string.Empty;

                if (item.Saldo.HasValue)
                {
                    saldoFormatted = item.TipoTransacao == TipoTransacao.Debito ? item.Saldo.Value.ToString("N2") : string.Format("({0})", item.Saldo.Value.ToString("N2"));
                }

                var masterModel = new MovimentoMasterModel(
                    movimentoId: item.MovimentoId,
                    tipoTransacao: item.TipoTransacao,
                    dataMovimento: item.DataMovimento.ToShortDateString(),
                    valorMovimento: item.TipoTransacao == TipoTransacao.Debito ? string.Format("({0})", item.ValorMovimento.ToString("N2")) : item.ValorMovimento.ToString("N2"),
                    historico: item.Historico,
                    saldo: saldoFormatted,
                    totalConciliado: item.TipoTransacao == TipoTransacao.Debito ? string.Format("({0})", item.TotalConciliado.ToString("N2")) : item.TotalConciliado.ToString("N2"),
                    estaConciliado: item.EstaConciliado,
                    movimentoImportacaoId: item.MovimentoImportacaoId
                );

                result.Add(masterModel);
            }

            return result;
        }

        #endregion
    }
}