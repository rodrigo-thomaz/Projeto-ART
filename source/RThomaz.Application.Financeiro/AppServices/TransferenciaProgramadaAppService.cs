using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class TransferenciaProgramadaAppService : AppServiceBase, ITransferenciaProgramadaAppService
    {
        #region private fields

        private readonly ITransferenciaProgramadaService _transferenciaProgramadaService;

        #endregion

        #region constructors

        public TransferenciaProgramadaAppService(ITransferenciaProgramadaService transferenciaProgramadaService)
        {
            _transferenciaProgramadaService = transferenciaProgramadaService;
        }

        #endregion

        #region public voids

        public async Task<TransferenciaProgramadaDetailViewModel> Edit(TransferenciaProgramadaDetailEditModel model)
        {
            var editDTO = Mapper.Map<TransferenciaProgramadaDetailEditModel, TransferenciaProgramadaDetailEditDTO>(model);
            var viewDTO = await _transferenciaProgramadaService.Edit(editDTO);
            var viewModel = Mapper.Map<TransferenciaProgramadaDetailViewDTO, TransferenciaProgramadaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<TransferenciaProgramadaDetailViewModel> GetDetail(long id)
        {
            var dto = await _transferenciaProgramadaService.GetDetail(id);
            var model = Mapper.Map<TransferenciaProgramadaDetailViewDTO, TransferenciaProgramadaDetailViewModel>(dto);
            return model;
        }

        public async Task<TransferenciaProgramadaDetailViewModel> Insert(TransferenciaProgramadaDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<TransferenciaProgramadaDetailInsertModel, TransferenciaProgramadaDetailInsertDTO>(model);
            var viewDTO = await _transferenciaProgramadaService.Insert(insertDTO);
            var viewModel = Mapper.Map<TransferenciaProgramadaDetailViewDTO, TransferenciaProgramadaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long programacaoId)
        {
            var result = await _transferenciaProgramadaService.Remove(programacaoId);
            return result;
        }

        #endregion
    }
}