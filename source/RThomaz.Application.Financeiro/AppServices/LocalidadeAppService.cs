using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.Storage;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class LocalidadeAppService : AppServiceBase, ILocalidadeAppService
    {
        #region private fields

        private readonly ILocalidadeService _localidadeService;

        #endregion

        #region constructors

        public LocalidadeAppService(ILocalidadeService localidadeService)
        {
            _localidadeService = localidadeService;
        }

        #endregion

        #region public voids

        public async Task<List<BairroSelectViewModel>> GetBairroSelectViewList(long cidadeId)
        {
            var selectListDTO = await _localidadeService.GetBairroSelectViewList(cidadeId);
            var selectListModel = Mapper.Map<List<BairroSelectViewDTO>, List<BairroSelectViewModel>>(selectListDTO);
            return selectListModel;
        }

        public async Task<List<CidadeSelectViewModel>> GetCidadeSelectViewList(long estadoId)
        {
            var selectListDTO = await _localidadeService.GetCidadeSelectViewList(estadoId);
            var selectListModel = Mapper.Map<List<CidadeSelectViewDTO>, List<CidadeSelectViewModel>>(selectListDTO);
            return selectListModel;
        }

        public async Task<List<EstadoSelectViewModel>> GetEstadoSelectViewList(long paisId)
        {
            var selectListDTO = await _localidadeService.GetEstadoSelectViewList(paisId);
            var selectListModel = Mapper.Map<List<EstadoSelectViewDTO>, List<EstadoSelectViewModel>>(selectListDTO);
            return selectListModel;
        }

        public async Task<StorageDownloadDTO> GetPaisBandeira(string storageObject)
        {
            var result = await _localidadeService.GetPaisBandeira(storageObject);            
            return result;
        }

        public async Task<List<PaisSelectViewModel>> GetPaisSelectViewList()
        {
            var selectListDTO = await _localidadeService.GetPaisSelectViewList();
            var selectListModel = Mapper.Map<List<PaisSelectViewDTO>, List<PaisSelectViewModel>>(selectListDTO);
            return selectListModel;
        }

        public async Task<BairroSelectViewModel> UpdateLocalidade(LocalidadeDetailUpdateModel localidade)
        {
            var localidadeDetailUpdateDTO = Mapper.Map<LocalidadeDetailUpdateModel, LocalidadeDetailUpdateDTO>(localidade);
            var bairroSelectViewDTO = await _localidadeService.UpdateLocalidade(localidadeDetailUpdateDTO);
            var result = Mapper.Map<BairroSelectViewDTO, BairroSelectViewModel>(bairroSelectViewDTO);
            return result;
        }

        #endregion
    }
}