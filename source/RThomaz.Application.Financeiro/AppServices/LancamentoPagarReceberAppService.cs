using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Enums;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class LancamentoPagarReceberAppService : AppServiceBase, ILancamentoPagarReceberAppService
    {
        #region private fields

        private readonly ILancamentoPagarReceberService _lancamentoPagarReceberService;

        #endregion

        #region constructors

        public LancamentoPagarReceberAppService(ILancamentoPagarReceberService lancamentoPagarReceberService)
        {
            _lancamentoPagarReceberService = lancamentoPagarReceberService;
        }

        #endregion

        #region public voids

        public async Task<LancamentoPagarReceberDetailViewModel> Edit(LancamentoPagarReceberDetailEditModel model)
        {
            var rateios = Mapper.Map<List<RateioDetailUpdateModel>, List<RateioDetailUpdateDTO>>(model.Rateios);

            List<ConciliacaoDetailUpdateDTO> conciliacoes = null;
            if (model.Conciliacoes != null)
            {
                foreach (var item in model.Conciliacoes)
                {
                    if (model.TipoTransacao == TipoTransacao.Debito)
                    {
                        item.ValorConciliado = item.ValorConciliado * (-1);
                    }
                }
                conciliacoes = Mapper.Map<List<ConciliacaoDetailUpdateModel>, List<ConciliacaoDetailUpdateDTO>>(model.Conciliacoes);
            }

            decimal valorVencimento = model.ValorVencimento;
            decimal? valorPagamento = model.EstaPago ? (decimal?)model.ValorPagamento : null;

            var dto = await _lancamentoPagarReceberService.Edit(new LancamentoPagarReceberDetailEditDTO
                (
                    lancamentoId: model.LancamentoId,
                    tipoTransacao: model.TipoTransacao,
                    pessoaId: model.PessoaId,
                    tipoPessoa: (TipoPessoa)model.TipoPessoa,
                    contaId: model.ContaId,
                    tipoConta: model.TipoConta,
                    dataVencimento: Convert.ToDateTime(model.DataVencimento),
                    valorVencimento: valorVencimento,
                    rateios: rateios,
                    conciliacoes: conciliacoes == null ? null : conciliacoes,
                    historico: model.Historico,
                    numero: model.Numero,
                    observacao: model.Observacao,
                    estaPago: model.EstaPago,
                    dataPagamento: model.EstaPago ? (DateTime?)Convert.ToDateTime(model.DataPagamento) : null,
                    valorPagamento: valorPagamento
                ));

            var result = ConvertDTOToModel(dto);

            return result;
        }

        public async Task<LancamentoPagarReceberDetailViewModel> GetDetail(long lancamentoId)
        {
            var dto = await _lancamentoPagarReceberService.GetDetail(lancamentoId);
            var model = ConvertDTOToModel(dto);
            return model;
        }

        public async Task<jQueryDataTableResult<List<LancamentoMasterModel>>> GetMasterList(jQueryDataTableParameter param, MesAnoModel periodo, long contaId, TipoConta tipoConta)
        {           
            var pagedListRequest = jQueryDataTableHelper<LancamentoPagarReceberMasterDTO>.ConvertToPagedListRequest(param);

            var periodoDTO = Mapper.Map<MesAnoModel, MesAnoDTO>(periodo);

            var masterContract = await _lancamentoPagarReceberService.GetMasterList(
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

        public async Task<List<MesAnoModel>> GetPeriodos(long contaId, TipoConta tipoConta)
        {
            var dtos = await _lancamentoPagarReceberService.GetPeriodos(contaId, tipoConta);
            var models = Mapper.Map<List<MesAnoDTO>, List<MesAnoModel>>(dtos);
            return models;
        }

        public async Task<LancamentoPagarReceberDetailViewModel> Insert(LancamentoPagarReceberDetailInsertModel model)
        {
            var rateios = Mapper.Map<List<RateioDetailUpdateModel>, List<RateioDetailUpdateDTO>>(model.Rateios);

            List<ConciliacaoDetailUpdateDTO> conciliacoes = null;
            if (model.Conciliacoes != null)
            {
                foreach (var item in model.Conciliacoes)
                {
                    if (model.TipoTransacao == TipoTransacao.Debito)
                    {
                        item.ValorConciliado = item.ValorConciliado * (-1);
                    }
                }
                conciliacoes = Mapper.Map<List<ConciliacaoDetailUpdateModel>, List<ConciliacaoDetailUpdateDTO>>(model.Conciliacoes);
            }

            decimal valorVencimento = model.ValorVencimento;
            decimal? valorPagamento = model.EstaPago ? model.ValorPagamento : null;

            var dto = await _lancamentoPagarReceberService.Insert(new LancamentoPagarReceberDetailInsertDTO
                (
                    tipoTransacao: model.TipoTransacao,
                    pessoaId: model.PessoaId,
                    tipoPessoa: (TipoPessoa)model.TipoPessoa,
                    contaId: model.ContaId,
                    tipoConta: model.TipoConta,
                    dataVencimento: model.DataVencimento,
                    valorVencimento: valorVencimento,
                    rateios: rateios,
                    conciliacoes: conciliacoes,
                    historico: model.Historico,
                    numero: model.Numero,
                    observacao: model.Observacao,
                    estaPago: model.EstaPago,
                    dataPagamento: model.EstaPago ? model.DataPagamento : null,
                    valorPagamento: valorPagamento
                ));

            var result = ConvertDTOToModel(dto);

            return result;
        }

        public async Task<bool> Remove(long lancamentoId, TipoTransacao tipoTransacao)
        {
            var result = await _lancamentoPagarReceberService.Remove(lancamentoId, tipoTransacao);
            return result;
        }

        #endregion

        #region private voids

        private LancamentoPagarReceberDetailViewModel ConvertDTOToModel(LancamentoPagarReceberDetailViewDTO dto)
        {
            PessoaSelectViewModel pessoa = null;

            if (dto.Pessoa != null)
            {
                pessoa = Mapper.Map<PessoaSelectViewDTO, PessoaSelectViewModel>(dto.Pessoa);
            }

            var conta = Mapper.Map<ContaSelectViewDTO, ContaSelectViewModel>(dto.Conta);
            var rateios = Mapper.Map<List<RateioDetailViewDTO>, List<RateioDetailViewModel>>(dto.Rateios);

            var model = new LancamentoPagarReceberDetailViewModel
            {
                LancamentoId = dto.LancamentoId,
                TipoTransacao = dto.TipoTransacao,
                Conta = conta,
                Pessoa = pessoa,
                Historico = dto.Historico,
                Numero = dto.Numero,
                DataVencimento = dto.DataVencimento,
                ValorVencimento = Math.Abs(dto.ValorVencimento),
                Rateios = rateios,
                Observacao = dto.Observacao,
            };

            return model;
        }

        private List<LancamentoMasterModel> ConvertToListOfMasterModel(List<LancamentoPagarReceberMasterDTO> collection)
        {
            var result = new List<LancamentoMasterModel>();

            foreach (var item in collection)
            {
                var masterModel = new LancamentoMasterModel(
                    tipoTransacao: item.TipoTransacao,
                    tipoProgramacao: item.ProgramacaoId != null ? "P" : item.TransferenciaId != null ? "T" : "",
                    dataVencimento: item.DataVencimento,
                    historico: item.Historico,
                    pessoaNome: item.PessoaNome,
                    valorVencimento: item.TipoTransacao == TipoTransacao.Debito ? string.Format("({0})", item.ValorVencimento.ToString("N2")) : item.ValorVencimento.ToString("N2"),
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