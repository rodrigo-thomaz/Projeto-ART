using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class MovimentoImportacaoOFXAppService : AppServiceBase, IMovimentoImportacaoOFXAppService
    {
        #region private fields

        private readonly IMovimentoImportacaoOFXService _movimentoImportacaoOFXService;

        #endregion

        #region constructors

        public MovimentoImportacaoOFXAppService(IMovimentoImportacaoOFXService movimentoImportacaoOFXService)
        {
            _movimentoImportacaoOFXService = movimentoImportacaoOFXService;
        }

        #endregion

        #region public voids

        public async Task<MovimentoImportacaoOFXModel> Preview(byte[] buffer)
        {
            var dto = await _movimentoImportacaoOFXService.Preview(buffer);
            var result = Mapper.Map<MovimentoImportacaoOFXDTO, MovimentoImportacaoOFXModel>(dto);
            return result;
        }

        public async Task<MovimentoImportacaoDetailViewModel> Import(MovimentoImportacaoOFXModel model)
        {
            var ofxDTO = Mapper.Map<MovimentoImportacaoOFXModel, MovimentoImportacaoOFXDTO>(model);
            var viewDTO = await _movimentoImportacaoOFXService.Import(ofxDTO);
            var viewModel = Mapper.Map<MovimentoImportacaoDetailViewDTO, MovimentoImportacaoDetailViewModel>(viewDTO);
            return viewModel;
        }

        #endregion
    }
}