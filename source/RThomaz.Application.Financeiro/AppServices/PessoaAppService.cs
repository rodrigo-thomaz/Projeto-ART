using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using AutoMapper;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class PessoaAppService : AppServiceBase, IPessoaAppService
    {
        #region private fields

        private readonly IPessoaService _pessoaService;

        #endregion

        #region constructors

        public PessoaAppService(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<PessoaMasterModel>>> GetMasterList(jQueryDataTableParameter param, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<PessoaMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _pessoaService.GetMasterList(pagedListRequest, ativo);
            var listOfMasterModel = Mapper.Map<List<PessoaMasterDTO>, List<PessoaMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<PessoaMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<PessoaFisicaDetailViewModel> EditPessoaFisica(PessoaFisicaDetailEditModel model)
        {
            var editDTO = Mapper.Map<PessoaFisicaDetailEditModel, PessoaFisicaDetailEditDTO>(model);
            var viewDTO = await _pessoaService.EditPessoaFisica(editDTO);
            var viewModel = Mapper.Map<PessoaFisicaDetailViewDTO, PessoaFisicaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<PessoaJuridicaDetailViewModel> EditPessoaJuridica(PessoaJuridicaDetailEditModel model)
        {
            var editDTO = Mapper.Map<PessoaJuridicaDetailEditModel, PessoaJuridicaDetailEditDTO>(model);
            var viewDTO = await _pessoaService.EditPessoaJuridica(editDTO);
            var viewModel = Mapper.Map<PessoaJuridicaDetailViewDTO, PessoaJuridicaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<PessoaFisicaDetailViewModel> GetPessoaFisicaDetail(long id)
        {
            var dto = await _pessoaService.GetPessoaFisicaDetail(id);
            var model = Mapper.Map<PessoaFisicaDetailViewDTO, PessoaFisicaDetailViewModel>(dto);
            return model;
        }

        public async Task<PessoaJuridicaDetailViewModel> GetPessoaJuridicaDetail(long id)
        {
            var dto = await _pessoaService.GetPessoaJuridicaDetail(id);
            var model = Mapper.Map<PessoaJuridicaDetailViewDTO, PessoaJuridicaDetailViewModel>(dto);
            return model;
        }

        public async Task<SelectListResponseModel<PessoaSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var selectListDTOResponse = await _pessoaService.GetSelectViewList(selectListRequestDTO);
            var selectListModel = Mapper.Map<List<PessoaSelectViewDTO>, List<PessoaSelectViewModel>>(selectListDTOResponse.Data);
            var selectListModelResponse = new SelectListResponseModel<PessoaSelectViewModel>(selectListDTOResponse.TotalRecords, selectListModel);
            return selectListModelResponse;
        }

        public async Task<PessoaFisicaDetailViewModel> InsertPessoaFisica(PessoaFisicaDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<PessoaFisicaDetailInsertModel, PessoaFisicaDetailInsertDTO>(model);
            var viewDTO = await _pessoaService.InsertPessoaFisica(insertDTO);
            var viewModel = Mapper.Map<PessoaFisicaDetailViewDTO, PessoaFisicaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<PessoaJuridicaDetailViewModel> InsertPessoaJuridica(PessoaJuridicaDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<PessoaJuridicaDetailInsertModel, PessoaJuridicaDetailInsertDTO>(model);
            var viewDTO = await _pessoaService.InsertPessoaJuridica(insertDTO);
            var viewModel = Mapper.Map<PessoaJuridicaDetailViewDTO, PessoaJuridicaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long pessoaId, TipoPessoa tipoPessoa)
        {
            var result = await _pessoaService.Remove(pessoaId, tipoPessoa);
            return result;
        }

        #endregion
    }
}