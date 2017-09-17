using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.Storage;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using System.Collections.Generic;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class BancoAppService : AppServiceBase, IBancoAppService
    {
        #region private fields

        private readonly IBancoService _bancoService;

        #endregion

        #region constructors

        public BancoAppService(IBancoService bancoService)
        {
            _bancoService = bancoService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<BancoMasterModel>>> GetMasterList(jQueryDataTableParameter param, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<BancoMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _bancoService.GetMasterList(pagedListRequest, ativo);
            var listOfMasterModel = Mapper.Map<List<BancoMasterDTO>, List<BancoMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<BancoMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<BancoDetailViewModel> Edit(BancoDetailEditModel model)
        {
            var editDTO = Mapper.Map<BancoDetailEditModel, BancoDetailEditDTO>(model);
            var viewDTO = await _bancoService.Edit(editDTO);
            var viewModel = Mapper.Map<BancoDetailViewDTO, BancoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<BancoDetailViewModel> GetDetail(long bancoId)
        {
            var dto = await _bancoService.GetDetail(bancoId);
            var model = Mapper.Map<BancoDetailViewDTO, BancoDetailViewModel>(dto);
            return model;
        }

        public async Task<StorageDownloadDTO> GetLogo(string storageObject)
        {
            var dto = await _bancoService.GetLogo(storageObject);
            return dto;
        }

        public async Task<BancoMascarasDetailViewModel> GetMascaras(long bancoId)
        {
            var dto = await _bancoService.GetMascaras(bancoId);
            var model = Mapper.Map<BancoMascarasDetailViewDTO, BancoMascarasDetailViewModel>(dto);
            return model;
        }

        public async Task<SelectListResponseModel<BancoSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var dtos = await _bancoService.GetSelectViewList(selectListRequestDTO);
            var models = Mapper.Map<List<BancoSelectViewDTO>, List<BancoSelectViewModel>>(dtos.Data);
            var response = new SelectListResponseModel<BancoSelectViewModel>(dtos.TotalRecords, models);
            return response;
        }

        public async Task<BancoDetailViewModel> Insert(BancoDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<BancoDetailInsertModel, BancoDetailInsertDTO>(model);
            var viewDTO = await _bancoService.Insert(insertDTO);
            var viewModel = Mapper.Map<BancoDetailViewDTO, BancoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long bancoId)
        {
            var result = await _bancoService.Remove(bancoId);
            return result;
        }

        public async Task<bool> UniqueNome(long? bancoId, string nome)
        {
            var result = await _bancoService.UniqueNome(bancoId, nome);
            return result;
        }

        public async Task<bool> UniqueNomeAbreviado(long? bancoId, string nomeAbreviado)
        {
            var result = await _bancoService.UniqueNomeAbreviado(bancoId, nomeAbreviado);
            return result;
        }

        public async Task<bool> UniqueNumero(long? bancoId, string numero)
        {
            var result = await _bancoService.UniqueNumero(bancoId, numero);
            return result;
        }

        #endregion
    }
}