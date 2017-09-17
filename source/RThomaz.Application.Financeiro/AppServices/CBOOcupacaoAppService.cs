using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Services.DTOs;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class CBOOcupacaoAppService : ICBOOcupacaoAppService
    {
        #region private fields

        private readonly ICBOOcupacaoService _cboOcupacaoService;

        #endregion

        #region constructors

        public CBOOcupacaoAppService(ICBOOcupacaoService cboOcupacaoService)
        {
            _cboOcupacaoService = cboOcupacaoService;
        }

        #endregion

        #region public voids

        public async Task<SelectListResponseModel<CBOOcupacaoSelectViewModel>> GetOcupacaoSelectViewList(SelectListRequestModel selectListModelRequest)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var selectListDTOResponse = await _cboOcupacaoService.GetOcupacaoSelectViewList(selectListRequestDTO);
            var selectListModel = Mapper.Map<List<CBOOcupacaoSelectViewDTO>, List<CBOOcupacaoSelectViewModel>>(selectListDTOResponse.Data);
            var selectListModelResponse = new SelectListResponseModel<CBOOcupacaoSelectViewModel>(selectListDTOResponse.TotalRecords, selectListModel);
            return selectListModelResponse;
        }

        public async Task<SelectListResponseModel<CBOSinonimoSelectViewModel>> GetSinonimoSelectViewList(SelectListRequestModel selectListModelRequest, int cboOcupacaoId)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var selectListDTOResponse = await _cboOcupacaoService.GetSinonimoSelectViewList(selectListRequestDTO, cboOcupacaoId);
            var selectListModel = Mapper.Map<List<CBOSinonimoSelectViewDTO>, List<CBOSinonimoSelectViewModel>>(selectListDTOResponse.Data);
            var selectListModelResponse = new SelectListResponseModel<CBOSinonimoSelectViewModel>(selectListDTOResponse.TotalRecords, selectListModel);
            return selectListModelResponse;
        }

        #endregion
    }
}