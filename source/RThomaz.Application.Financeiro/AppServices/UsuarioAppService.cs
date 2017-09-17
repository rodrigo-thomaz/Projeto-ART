using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Services.DTOs;
using AutoMapper;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class UsuarioAppService : AppServiceBase, IUsuarioAppService
    {
        #region private fields

        private readonly IUsuarioService _usuarioService;

        #endregion

        #region constructors

        public UsuarioAppService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<UsuarioMasterModel>>> GetMasterList(jQueryDataTableParameter param, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<UsuarioMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _usuarioService.GetMasterList(pagedListRequest, ativo);
            var listOfMasterModel = Mapper.Map<List<UsuarioMasterDTO>, List<UsuarioMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<UsuarioMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<UsuarioDetailViewModel> Edit(UsuarioDetailEditModel model)
        {
            var editDTO = Mapper.Map<UsuarioDetailEditModel, UsuarioDetailEditDTO>(model);
            var viewDTO = await _usuarioService.Edit(editDTO);
            var viewModel = Mapper.Map<UsuarioDetailViewDTO, UsuarioDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<UsuarioDetailViewModel> GetDetail(long usuarioId)
        {
            var dto = await _usuarioService.GetDetail(usuarioId);
            var model = Mapper.Map<UsuarioDetailViewDTO, UsuarioDetailViewModel>(dto);
            return model;
        }

        public async Task<SelectListResponseModel<UsuarioSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var selectListDTOResponse = await _usuarioService.GetSelectViewList(selectListRequestDTO);
            var selectListModel = Mapper.Map<List<UsuarioSelectViewDTO>, List<UsuarioSelectViewModel>>(selectListDTOResponse.Data);
            var selectListModelResponse = new SelectListResponseModel<UsuarioSelectViewModel>(selectListDTOResponse.TotalRecords, selectListModel);
            return selectListModelResponse;
        }

        public async Task<UsuarioDetailViewModel> Insert(UsuarioDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<UsuarioDetailInsertModel, UsuarioDetailInsertDTO>(model);
            var viewDTO = await _usuarioService.Insert(insertDTO);
            var viewModel = Mapper.Map<UsuarioDetailViewDTO, UsuarioDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long usuarioId)
        {
            var result = await _usuarioService.Remove(usuarioId);
            return result;
        }

        public async Task<bool> UniqueEmail(string email)
        {
            var result = await _usuarioService.UniqueEmail(email);
            return result;
        }

        #endregion
    }
}