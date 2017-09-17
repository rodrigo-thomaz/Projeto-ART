using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Helpers;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Financeiro.Services.DTOs;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class ContaAppService : AppServiceBase, IContaAppService
    {
        #region private fields

        private readonly IContaService _contaService;

        #endregion

        #region constructors

        public ContaAppService(IContaService contaService)
        {
            _contaService = contaService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<ContaMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoConta? tipoConta, bool? ativo)
        {
            var pagedListRequest = jQueryDataTableHelper<ContaMasterDTO>.ConvertToPagedListRequest(param);
            var masterContract = await _contaService.GetMasterList(pagedListRequest, tipoConta, ativo);
            var listOfMasterModel = Mapper.Map<List<ContaMasterDTO>, List<ContaMasterModel>>(masterContract.Data);
            var result = new jQueryDataTableResult<List<ContaMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<ContaCartaoCreditoDetailViewModel> EditContaCartaoCredito(ContaCartaoCreditoDetailEditModel model)
        {
            var editDTO = Mapper.Map<ContaCartaoCreditoDetailEditModel, ContaCartaoCreditoDetailEditDTO>(model);
            var viewDTO = await _contaService.EditContaCartaoCredito(editDTO);
            var viewModel = Mapper.Map<ContaCartaoCreditoDetailViewDTO, ContaCartaoCreditoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<ContaCorrenteDetailViewModel> EditContaCorrente(ContaCorrenteDetailEditModel model)
        {
            var editDTO = Mapper.Map<ContaCorrenteDetailEditModel, ContaCorrenteDetailEditDTO>(model);
            var viewDTO = await _contaService.EditContaCorrente(editDTO);
            var viewModel = Mapper.Map<ContaCorrenteDetailViewDTO, ContaCorrenteDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<ContaEspecieDetailViewModel> EditContaEspecie(ContaEspecieDetailEditModel model)
        {
            var editDTO = Mapper.Map<ContaEspecieDetailEditModel, ContaEspecieDetailEditDTO>(model);
            var viewDTO = await _contaService.EditContaEspecie(editDTO);
            var viewModel = Mapper.Map<ContaEspecieDetailViewDTO, ContaEspecieDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<ContaPoupancaDetailViewModel> EditContaPoupanca(ContaPoupancaDetailEditModel model)
        {
            var editDTO = Mapper.Map<ContaPoupancaDetailEditModel, ContaPoupancaDetailEditDTO>(model);
            var viewDTO = await _contaService.EditContaPoupanca(editDTO);
            var viewModel = Mapper.Map<ContaPoupancaDetailViewDTO, ContaPoupancaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<ContaCartaoCreditoDetailViewModel> GetContaCartaoCreditoDetail(long id)
        {
            var dto = await _contaService.GetContaCartaoCreditoDetail(id);
            var model = Mapper.Map<ContaCartaoCreditoDetailViewDTO, ContaCartaoCreditoDetailViewModel>(dto);
            return model;
        }

        public async Task<ContaCorrenteDetailViewModel> GetContaCorrenteDetail(long id)
        {
            var dto = await _contaService.GetContaCorrenteDetail(id);
            var model = Mapper.Map<ContaCorrenteDetailViewDTO, ContaCorrenteDetailViewModel>(dto);
            return model;
        }

        public async Task<ContaEspecieDetailViewModel> GetContaEspecieDetail(long id)
        {
            var dto = await _contaService.GetContaEspecieDetail(id);
            var model = Mapper.Map<ContaEspecieDetailViewDTO, ContaEspecieDetailViewModel>(dto);
            return model;
        }

        public async Task<ContaPoupancaDetailViewModel> GetContaPoupancaDetail(long id)
        {
            var dto = await _contaService.GetContaPoupancaDetail(id);
            var model = Mapper.Map<ContaPoupancaDetailViewDTO, ContaPoupancaDetailViewModel>(dto);
            return model;
        }

        public async Task<List<ContaSelectViewModel>> GetSelectViewList(TipoConta? tipoConta)
        {
            var selectListDTO = await _contaService.GetSelectViewList(tipoConta);
            var selectListModel = Mapper.Map<List<ContaSelectViewDTO>, List<ContaSelectViewModel>>(selectListDTO);
            return selectListModel;
        }

        public async Task<List<ContaSummaryViewModel>> GetSummaryViewList()
        {
            var summaryListDTO = await _contaService.GetSummaryViewList();
            var selectListModel = Mapper.Map<List<ContaSummaryViewDTO>, List<ContaSummaryViewModel>>(summaryListDTO);
            return selectListModel;
        }

        public async Task<List<CountModel<ContaSelectViewModel>>> GetWithProgramacaoSelectViewList()
        {            
            var selectListDTO = await _contaService.GetWithProgramacaoSelectViewList();
            var selectListModel = ConvertDTOToModel(selectListDTO);
            return selectListModel;
        }

        public async Task<ContaCartaoCreditoDetailViewModel> InsertContaCartaoCredito(ContaCartaoCreditoDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<ContaCartaoCreditoDetailInsertModel, ContaCartaoCreditoDetailInsertDTO>(model);
            var viewDTO = await _contaService.InsertContaCartaoCredito(insertDTO);
            var viewModel = Mapper.Map<ContaCartaoCreditoDetailViewDTO, ContaCartaoCreditoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<ContaCorrenteDetailViewModel> InsertContaCorrente(ContaCorrenteDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<ContaCorrenteDetailInsertModel, ContaCorrenteDetailInsertDTO>(model);
            var viewDTO = await _contaService.InsertContaCorrente(insertDTO);
            var viewModel = Mapper.Map<ContaCorrenteDetailViewDTO, ContaCorrenteDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<ContaEspecieDetailViewModel> InsertContaEspecie(ContaEspecieDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<ContaEspecieDetailInsertModel, ContaEspecieDetailInsertDTO>(model);
            var viewDTO = await _contaService.InsertContaEspecie(insertDTO);
            var viewModel = Mapper.Map<ContaEspecieDetailViewDTO, ContaEspecieDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<ContaPoupancaDetailViewModel> InsertContaPoupanca(ContaPoupancaDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<ContaPoupancaDetailInsertModel, ContaPoupancaDetailInsertDTO>(model);
            var viewDTO = await _contaService.InsertContaPoupanca(insertDTO);
            var viewModel = Mapper.Map<ContaPoupancaDetailViewDTO, ContaPoupancaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long contaId, TipoConta tipoConta)
        {
            var result = await _contaService.Remove(contaId, tipoConta);
            return result;
        }

        #endregion

        #region private voids

        private static List<CountModel<ContaSelectViewModel>> ConvertDTOToModel(List<CountDTO<ContaSelectViewDTO>> dtos)
        {
            var result = new List<CountModel<ContaSelectViewModel>>();

            foreach (var item in dtos)
            {
                var conta = Mapper.Map<ContaSelectViewDTO, ContaSelectViewModel>(item.DTO);
                result.Add(new CountModel<ContaSelectViewModel>(conta, item.Count));
            }

            return result;
        }                

        #endregion
    }
}