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
    public class TipoTelefoneAppService : AppServiceBase, ITipoTelefoneAppService
    {
        #region private voids

        private readonly ITipoTelefoneService _tipoTelefoneService;

        #endregion

        #region constructors

        public TipoTelefoneAppService(ITipoTelefoneService tipoTelefoneService)
        {
            _tipoTelefoneService = tipoTelefoneService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<TipoTelefoneMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoPessoa tipoPessoa, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<TipoTelefoneMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _tipoTelefoneService.GetMasterList(pagedListRequest, tipoPessoa, ativo);
            var listOfMasterModel = Mapper.Map<List<TipoTelefoneMasterDTO>, List<TipoTelefoneMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<TipoTelefoneMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<TipoTelefoneDetailViewModel> GetDetail(Guid id, TipoPessoa tipoPessoa)
        {
            var dto = await _tipoTelefoneService.GetDetail(id, tipoPessoa);
            var model = Mapper.Map<TipoTelefoneDetailViewDTO, TipoTelefoneDetailViewModel>(dto);
            return model;
        }

        public async Task<TipoTelefoneDetailViewModel> Edit(TipoTelefoneDetailEditModel model)
        {
            var editDTO = Mapper.Map<TipoTelefoneDetailEditModel, TipoTelefoneDetailEditDTO>(model);
            var viewDTO = await _tipoTelefoneService.Edit(editDTO);
            var viewModel = Mapper.Map<TipoTelefoneDetailViewDTO, TipoTelefoneDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<List<TipoTelefoneSelectViewModel>> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            var dtos = await _tipoTelefoneService.GetSelectViewList(tipoPessoa);
            var models = Mapper.Map<List<TipoTelefoneSelectViewDTO>, List<TipoTelefoneSelectViewModel>>(dtos);
            return models;
        }

        public async Task<TipoTelefoneDetailViewModel> Insert(TipoTelefoneDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<TipoTelefoneDetailInsertModel, TipoTelefoneDetailInsertDTO>(model);
            var viewDTO = await _tipoTelefoneService.Insert(insertDTO);
            var viewModel = Mapper.Map<TipoTelefoneDetailViewDTO, TipoTelefoneDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(Guid tipoTelefoneId, TipoPessoa tipoPessoa)
        {
            var result = await _tipoTelefoneService.Remove(tipoTelefoneId, tipoPessoa);
            return result;
        }

        public async Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoTelefoneService.UniqueNome(tipoPessoa, nome);
            return result;
        }

        public async Task<bool> UniqueNome(Guid tipoTelefoneId, TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoTelefoneService.UniqueNome(tipoTelefoneId, tipoPessoa, nome);
            return result;
        }

        #endregion
    }
}
