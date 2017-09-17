using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;
using AutoMapper;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class MovimentoImportacaoAppService : AppServiceBase, IMovimentoImportacaoAppService
    {
        #region private fields

        private readonly IMovimentoImportacaoService _movimentoImportacaoService;

        #endregion

        #region constructors

        public MovimentoImportacaoAppService(IMovimentoImportacaoService movimentoImportacaoService)
        {
            _movimentoImportacaoService = movimentoImportacaoService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<MovimentoImportacaoMasterModel>>> GetMasterList(jQueryDataTableParameter param, long movimentoImportacaoId, TipoTransacao? tipoTransacao)
        {
            var pagedListRequest = jQueryDataTableHelper<MovimentoImportacaoMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _movimentoImportacaoService.GetMasterList(pagedListRequest, movimentoImportacaoId, tipoTransacao);
            var listOfMasterModel = Mapper.Map<List<MovimentoImportacaoMasterDTO>, List<MovimentoImportacaoMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<MovimentoImportacaoMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<SelectListResponseModel<MovimentoImportacaoSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var dtos = await _movimentoImportacaoService.GetSelectViewList(selectListRequestDTO);
            var models = Mapper.Map<List<MovimentoImportacaoSelectViewDTO>, List<MovimentoImportacaoSelectViewModel>>(dtos.Data);
            var response = new SelectListResponseModel<MovimentoImportacaoSelectViewModel>(dtos.TotalRecords, models);
            return response;
        }

        public async Task<bool> Remove(long movimentoImportacaoId)
        {
            var result = await _movimentoImportacaoService.Remove(movimentoImportacaoId);
            return result;
        }

        #endregion
    }
}