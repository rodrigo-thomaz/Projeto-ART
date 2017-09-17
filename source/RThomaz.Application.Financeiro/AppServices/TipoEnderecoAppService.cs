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
    public class TipoEnderecoAppService : AppServiceBase, ITipoEnderecoAppService
    {
        #region private voids

        private readonly ITipoEnderecoService _tipoEnderecoService;

        #endregion

        #region constructors

        public TipoEnderecoAppService(ITipoEnderecoService tipoEnderecoService)
        {
            _tipoEnderecoService = tipoEnderecoService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<TipoEnderecoMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoPessoa tipoPessoa, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<TipoEnderecoMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _tipoEnderecoService.GetMasterList(pagedListRequest, tipoPessoa, ativo);
            var listOfMasterModel = Mapper.Map<List<TipoEnderecoMasterDTO>, List<TipoEnderecoMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<TipoEnderecoMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<TipoEnderecoDetailViewModel> GetDetail(Guid id, TipoPessoa tipoPessoa)
        {
            var dto = await _tipoEnderecoService.GetDetail(id, tipoPessoa);
            var model = Mapper.Map<TipoEnderecoDetailViewDTO, TipoEnderecoDetailViewModel>(dto);
            return model;
        }

        public async Task<TipoEnderecoDetailViewModel> Edit(TipoEnderecoDetailEditModel model)
        {
            var editDTO = Mapper.Map<TipoEnderecoDetailEditModel, TipoEnderecoDetailEditDTO>(model);
            var viewDTO = await _tipoEnderecoService.Edit(editDTO);
            var viewModel = Mapper.Map<TipoEnderecoDetailViewDTO, TipoEnderecoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<List<TipoEnderecoSelectViewModel>> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            var dtos = await _tipoEnderecoService.GetSelectViewList(tipoPessoa);
            var models = Mapper.Map<List<TipoEnderecoSelectViewDTO>, List<TipoEnderecoSelectViewModel>>(dtos);
            return models;
        }

        public async Task<TipoEnderecoDetailViewModel> Insert(TipoEnderecoDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<TipoEnderecoDetailInsertModel, TipoEnderecoDetailInsertDTO>(model);
            var viewDTO = await _tipoEnderecoService.Insert(insertDTO);
            var viewModel = Mapper.Map<TipoEnderecoDetailViewDTO, TipoEnderecoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(Guid tipoEnderecoId, TipoPessoa tipoPessoa)
        {
            var result = await _tipoEnderecoService.Remove(tipoEnderecoId, tipoPessoa);
            return result;
        }

        public async Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoEnderecoService.UniqueNome(tipoPessoa, nome);
            return result;
        }

        public async Task<bool> UniqueNome(Guid tipoEnderecoId, TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoEnderecoService.UniqueNome(tipoEnderecoId, tipoPessoa, nome);
            return result;
        }

        #endregion
    }
}
