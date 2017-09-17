using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using AutoMapper;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class LancamentoPagoRecebidoAppService : AppServiceBase, ILancamentoPagoRecebidoAppService
    {
        #region private fields

        private readonly ILancamentoPagoRecebidoService _lancamentoPagoRecebidoService;

        #endregion

        #region constructors

        public LancamentoPagoRecebidoAppService(ILancamentoPagoRecebidoService lancamentoPagoRecebidoService)
        {
            _lancamentoPagoRecebidoService = lancamentoPagoRecebidoService;
        }

        #endregion

        #region public voids

        public async Task<jQueryDataTableResult<List<LancamentoMasterModel>>> GetMasterList(jQueryDataTableParameter param, MesAnoModel periodo, long contaId, TipoConta tipoConta)
        {
            var pagedListRequest = jQueryDataTableHelper<LancamentoPagoRecebidoMasterDTO>.ConvertToPagedListRequest(param);

            var periodoDTO = Mapper.Map<MesAnoModel, MesAnoDTO>(periodo);

            var masterContract = await _lancamentoPagoRecebidoService.GetMasterList(
                pagedListRequest,
                periodoDTO,
                contaId,
                tipoConta);

            var listOfMasterModel = ConvertToListOfMasterModel(masterContract.PagedListResponse.Data);

            var result = new jQueryDataTableResult<List<LancamentoMasterModel>>
            (
                echo: param.draw,
                totalRecords: masterContract.PagedListResponse.TotalRecords,
                totalDisplayRecords: masterContract.PagedListResponse.TotalRecords,
                data: listOfMasterModel
            );
            return result;
        }

        public async Task<LancamentoPagoRecebidoDetailViewModel> Edit(LancamentoPagoRecebidoDetailEditModel model)
        {
            var editDTO = Mapper.Map<LancamentoPagoRecebidoDetailEditModel, LancamentoPagoRecebidoDetailEditDTO>(model);
            var viewDTO = await _lancamentoPagoRecebidoService.Edit(editDTO);
            var viewModel = Mapper.Map<LancamentoPagoRecebidoDetailViewDTO, LancamentoPagoRecebidoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<LancamentoPagoRecebidoDetailViewModel> GetDetail(long lancamentoId)
        {
            var dto = await _lancamentoPagoRecebidoService.GetDetail(lancamentoId);
            var result = Mapper.Map<LancamentoPagoRecebidoDetailViewDTO, LancamentoPagoRecebidoDetailViewModel>(dto);
            return result;
        }

        public async Task<List<MesAnoModel>> GetPeriodos(long contaId, TipoConta tipoConta)
        {
            var dtos = await _lancamentoPagoRecebidoService.GetPeriodos(contaId, tipoConta);
            var result = Mapper.Map<List<MesAnoDTO>, List<MesAnoModel>>(dtos);
            return result;
        }

        public async Task<bool> Remove(long lancamentoId, TipoTransacao tipoTransacao)
        {
            var result = await _lancamentoPagoRecebidoService.Remove(lancamentoId, tipoTransacao);
            return result;
        }

        #endregion

        #region private voids

        private LancamentoPagoRecebidoDetailViewModel ConvertDTOToModel(LancamentoPagoRecebidoDetailViewDTO dto)
        {
            var result = Mapper.Map<LancamentoPagoRecebidoDetailViewDTO, LancamentoPagoRecebidoDetailViewModel>(dto);
            return result;
        }

        private List<LancamentoMasterModel> ConvertToListOfMasterModel(List<LancamentoPagoRecebidoMasterDTO> collection)
        {
            var result = new List<LancamentoMasterModel>();

            foreach (var item in collection)
            {
                var masterModel = new LancamentoMasterModel(
                    tipoTransacao: item.TipoTransacao,
                    tipoProgramacao: item.ProgramacaoId != null ? "P" : item.TransferenciaId != null ? "T" : "",
                    dataVencimento: item.DataPagamento,
                    historico: item.Historico,
                    pessoaNome: item.PessoaNome,
                    valorVencimento: item.TipoTransacao == TipoTransacao.Debito ? string.Format("({0})", item.ValorPagamento.ToString("N2")) : item.ValorPagamento.ToString("N2"),
                    saldo: item.Saldo >= 0 ? item.Saldo.ToString("N2") : string.Format("({0})", item.Saldo.ToString("N2")),
                    lancamentoId: item.LancamentoId,
                    transferenciaId: item.TransferenciaId,
                    programacaoId: item.ProgramacaoId
                );

                result.Add(masterModel);
            }

            return result;
        }

        #endregion
    }
}