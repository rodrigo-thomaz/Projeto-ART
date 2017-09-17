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
    public class TipoHomePageAppService : AppServiceBase, ITipoHomePageAppService
    {
        #region private voids

        private readonly ITipoHomePageService _tipoHomePageService;

        #endregion

        #region constructors

        public TipoHomePageAppService(ITipoHomePageService tipoHomePageService)
        {
            _tipoHomePageService = tipoHomePageService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<TipoHomePageMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoPessoa tipoPessoa, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<TipoHomePageMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _tipoHomePageService.GetMasterList(pagedListRequest, tipoPessoa, ativo);
            var listOfMasterModel = Mapper.Map<List<TipoHomePageMasterDTO>, List<TipoHomePageMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<TipoHomePageMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<TipoHomePageDetailViewModel> GetDetail(Guid id, TipoPessoa tipoPessoa)
        {
            var dto = await _tipoHomePageService.GetDetail(id, tipoPessoa);
            var model = Mapper.Map<TipoHomePageDetailViewDTO, TipoHomePageDetailViewModel>(dto);
            return model;
        }

        public async Task<TipoHomePageDetailViewModel> Edit(TipoHomePageDetailEditModel model)
        {
            var editDTO = Mapper.Map<TipoHomePageDetailEditModel, TipoHomePageDetailEditDTO>(model);
            var viewDTO = await _tipoHomePageService.Edit(editDTO);
            var viewModel = Mapper.Map<TipoHomePageDetailViewDTO, TipoHomePageDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<List<TipoHomePageSelectViewModel>> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            var dtos = await _tipoHomePageService.GetSelectViewList(tipoPessoa);
            var models = Mapper.Map<List<TipoHomePageSelectViewDTO>, List<TipoHomePageSelectViewModel>>(dtos);
            return models;
        }

        public async Task<TipoHomePageDetailViewModel> Insert(TipoHomePageDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<TipoHomePageDetailInsertModel, TipoHomePageDetailInsertDTO>(model);
            var viewDTO = await _tipoHomePageService.Insert(insertDTO);
            var viewModel = Mapper.Map<TipoHomePageDetailViewDTO, TipoHomePageDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(Guid tipoHomePageId, TipoPessoa tipoPessoa)
        {
            var result = await _tipoHomePageService.Remove(tipoHomePageId, tipoPessoa);
            return result;
        }

        public async Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoHomePageService.UniqueNome(tipoPessoa, nome);
            return result;
        }

        public async Task<bool> UniqueNome(Guid tipoHomePageId, TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoHomePageService.UniqueNome(tipoHomePageId, tipoPessoa, nome);
            return result;
        }

        #endregion
    }
}
