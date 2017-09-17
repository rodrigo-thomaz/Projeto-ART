using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class TransferenciaAppService : AppServiceBase, ITransferenciaAppService
    {
        #region private fields

        private readonly ITransferenciaService _transferenciaService;

        #endregion

        #region constructors

        public TransferenciaAppService(ITransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        #endregion

        #region public voids

        public async Task<TransferenciaDetailViewModel> Edit(TransferenciaDetailEditModel model)
        {
            var editDTO = Mapper.Map<TransferenciaDetailEditModel, TransferenciaDetailEditDTO>(model);
            var viewDTO = await _transferenciaService.Edit(editDTO);
            var viewModel = Mapper.Map<TransferenciaDetailViewDTO, TransferenciaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<TransferenciaDetailViewModel> GetDetail(long transferenciaId)
        {
            var dto = await _transferenciaService.GetDetail(transferenciaId);
            var model = Mapper.Map<TransferenciaDetailViewDTO, TransferenciaDetailViewModel>(dto);
            return model;
        }

        public async Task<TransferenciaDetailViewModel> Insert(TransferenciaDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<TransferenciaDetailInsertModel, TransferenciaDetailInsertDTO>(model);
            var viewDTO = await _transferenciaService.Insert(insertDTO);
            var viewModel = Mapper.Map<TransferenciaDetailViewDTO, TransferenciaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long transferenciaId)
        {
            var result = await _transferenciaService.Remove(transferenciaId);
            return result;
        }

        #endregion
    }
}