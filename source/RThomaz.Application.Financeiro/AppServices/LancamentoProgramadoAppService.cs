using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class LancamentoProgramadoAppService : AppServiceBase, ILancamentoProgramadoAppService
    {
        #region private fields

        private readonly ILancamentoProgramadoService _lancamentoProgramadoService;

        #endregion

        #region constructors

        public LancamentoProgramadoAppService(ILancamentoProgramadoService lancamentoProgramadoService)
        {
            _lancamentoProgramadoService = lancamentoProgramadoService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<LancamentoProgramadoMasterModel>>> GetMasterList(jQueryDataTableParameter param, long? contaId, TipoConta? tipoConta)
        {
            var pagedListRequest = jQueryDataTableHelper<ProgramacaoMasterDTO>.ConvertToPagedListRequest(param);

            var masterContract = await _lancamentoProgramadoService.GetMasterList
                (
                      pagedListRequest
                    , contaId
                    , tipoConta
                );

            var listOfMasterModel = ConvertToListOfMasterModel(masterContract.Data);

            var result = new jQueryDataTableResult<List<LancamentoProgramadoMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.TotalRecords,
                totalDisplayRecords: masterContract.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<LancamentoProgramadoDetailViewModel> GetDetail(long programacaoId)
        {
            var dto = await _lancamentoProgramadoService.GetDetail(programacaoId);
            var result = Mapper.Map<LancamentoProgramadoDetailViewDTO, LancamentoProgramadoDetailViewModel>(dto);
            return result;
        }

        public async Task<LancamentoProgramadoDetailViewModel> Insert(LancamentoProgramadoDetailUpdateModel model)
        {
            var insertDTO = Mapper.Map<LancamentoProgramadoDetailUpdateModel, LancamentoProgramadoInsertDTO>(model);
            var viewDTO = await _lancamentoProgramadoService.Insert(insertDTO);
            var viewModel = Mapper.Map<LancamentoProgramadoDetailViewDTO, LancamentoProgramadoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<LancamentoProgramadoDetailViewModel> Edit(LancamentoProgramadoDetailUpdateModel model)
        {
            var editDTO = Mapper.Map<LancamentoProgramadoDetailUpdateModel, LancamentoProgramadoEditDTO>(model);
            var viewDTO = await _lancamentoProgramadoService.Edit(editDTO);
            var viewModel = Mapper.Map<LancamentoProgramadoDetailViewDTO, LancamentoProgramadoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<bool> Remove(long id)
        {
            var result = await _lancamentoProgramadoService.Remove(id);
            return result;
        }

        #endregion

        #region private voids

        private List<LancamentoProgramadoMasterModel> ConvertToListOfMasterModel(List<ProgramacaoMasterDTO> collection)
        {
            var result = new List<LancamentoProgramadoMasterModel>();

            foreach (var item in collection)
            {
                var masterModel = new LancamentoProgramadoMasterModel(
                    programacaoId: item.ProgramacaoId,
                    frequencia: item.Frequencia.ToString(),
                    dataInicial: item.DataInicial.ToShortDateString(),
                    dataFinal: item.DataFinal.ToShortDateString(),
                    historico: item.Historico,
                    valorVencimento: item.ValorVencimento >= 0 ? item.ValorVencimento.ToString("N2") : string.Format("({0})", item.ValorVencimento.ToString("N2")),
                    tipoTransacao: item.TipoTransacao != null ? ((byte)item.TipoTransacao).ToString() : string.Empty,
                    pessoaNome: item.PessoaNome,
                    contaNome: item.ContaNome
                );

                result.Add(masterModel);
            }

            return result;
        }

        #endregion
    }
}