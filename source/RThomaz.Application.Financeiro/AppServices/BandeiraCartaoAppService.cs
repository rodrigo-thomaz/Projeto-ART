using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.Storage;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class BandeiraCartaoAppService : AppServiceBase, IBandeiraCartaoAppService
    {
        #region private fields

        private readonly IBandeiraCartaoService _bandeiraCartaoService;

        #endregion

        #region constructors

        public BandeiraCartaoAppService(IBandeiraCartaoService bandeiraCartaoService)
        {
            _bandeiraCartaoService = bandeiraCartaoService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<BandeiraCartaoMasterModel>>> GetMasterList(jQueryDataTableParameter param, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<BandeiraCartaoMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _bandeiraCartaoService.GetMasterList(pagedListRequest, ativo);
            var listOfMasterModel = Mapper.Map<List<BandeiraCartaoMasterDTO>, List<BandeiraCartaoMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<BandeiraCartaoMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<BandeiraCartaoDetailViewModel> Edit(BandeiraCartaoDetailEditModel model)
        {
            var editDTO = Mapper.Map<BandeiraCartaoDetailEditModel, BandeiraCartaoDetailEditDTO>(model);
            var viewDTO = await _bandeiraCartaoService.Edit(editDTO);
            var viewModel = Mapper.Map<BandeiraCartaoDetailViewDTO, BandeiraCartaoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<BandeiraCartaoDetailViewModel> GetDetail(long id)
        {
            var dto = await _bandeiraCartaoService.GetDetail(id);
            var model = Mapper.Map<BandeiraCartaoDetailViewDTO, BandeiraCartaoDetailViewModel>(dto);
            return model;
        }

        public async Task<StorageDownloadDTO> GetLogo(string storageObject)
        {
            var dto = await _bandeiraCartaoService.GetLogo(storageObject);
            return dto;
        }

        public async Task<List<BandeiraCartaoSelectViewModel>> GetSelectViewList()
        {
            var selectListDTO = await _bandeiraCartaoService.GetSelectViewList();
            var selectListModel = Mapper.Map<List<BandeiraCartaoSelectViewDTO>, List<BandeiraCartaoSelectViewModel>>(selectListDTO);
            return selectListModel;
        }

        public async Task<BandeiraCartaoDetailViewModel> Insert(BandeiraCartaoDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<BandeiraCartaoDetailInsertModel, BandeiraCartaoDetailInsertDTO>(model);
            var viewDTO = await _bandeiraCartaoService.Insert(insertDTO);
            var viewModel = Mapper.Map<BandeiraCartaoDetailViewDTO, BandeiraCartaoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long id)
        {
            var result = await _bandeiraCartaoService.Remove(id);
            return result;
        }

        public async Task<bool> UniqueNome(long? bandeiraCartaoId, string nome)
        {
            var result = await _bandeiraCartaoService.UniqueNome(bandeiraCartaoId, nome);
            return result;
        }

        #endregion
    }
}