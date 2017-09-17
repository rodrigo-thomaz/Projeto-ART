using AutoMapper;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Enums;
using System.Threading.Tasks;
using System.Collections.Generic;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using System;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class TipoEmailAppService : AppServiceBase, ITipoEmailAppService
    {
        #region private voids

        private readonly ITipoEmailService _tipoEmailService;

        #endregion

        #region constructors

        public TipoEmailAppService(ITipoEmailService tipoEmailService)
        {
            _tipoEmailService = tipoEmailService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<TipoEmailMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoPessoa tipoPessoa, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<TipoEmailMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _tipoEmailService.GetMasterList(pagedListRequest, tipoPessoa, ativo);
            var listOfMasterModel = Mapper.Map<List<TipoEmailMasterDTO>, List<TipoEmailMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<TipoEmailMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<TipoEmailDetailViewModel> GetDetail(Guid id, TipoPessoa tipoPessoa)
        {
            var dto = await _tipoEmailService.GetDetail(id, tipoPessoa);
            var model = Mapper.Map<TipoEmailDetailViewDTO, TipoEmailDetailViewModel>(dto);
            return model;
        }

        public async Task<TipoEmailDetailViewModel> Edit(TipoEmailDetailEditModel model)
        {
            var editDTO = Mapper.Map<TipoEmailDetailEditModel, TipoEmailDetailEditDTO>(model);
            var viewDTO = await _tipoEmailService.Edit(editDTO);
            var viewModel = Mapper.Map<TipoEmailDetailViewDTO, TipoEmailDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<List<TipoEmailSelectViewModel>> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            var dtos = await _tipoEmailService.GetSelectViewList(tipoPessoa);
            var models = Mapper.Map<List<TipoEmailSelectViewDTO>, List<TipoEmailSelectViewModel>>(dtos);
            return models;
        }

        public async Task<TipoEmailDetailViewModel> Insert(TipoEmailDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<TipoEmailDetailInsertModel, TipoEmailDetailInsertDTO>(model);
            var viewDTO = await _tipoEmailService.Insert(insertDTO);
            var viewModel = Mapper.Map<TipoEmailDetailViewDTO, TipoEmailDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(Guid tipoEmailId, TipoPessoa tipoPessoa)
        {
            var result = await _tipoEmailService.Remove(tipoEmailId, tipoPessoa);
            return result;
        }

        public async Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoEmailService.UniqueNome(tipoPessoa, nome);
            return result;
        }

        public async Task<bool> UniqueNome(Guid tipoEmailId, TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoEmailService.UniqueNome(tipoEmailId, tipoPessoa, nome);
            return result;
        }

        #endregion
    }
}
