using RThomaz.Application.Financeiro.Helpers;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IContaAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<ContaMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoConta? tipoConta, bool? ativo);
        Task<ContaCartaoCreditoDetailViewModel> EditContaCartaoCredito(ContaCartaoCreditoDetailEditModel model);
        Task<ContaCorrenteDetailViewModel> EditContaCorrente(ContaCorrenteDetailEditModel model);
        Task<ContaEspecieDetailViewModel> EditContaEspecie(ContaEspecieDetailEditModel model);
        Task<ContaPoupancaDetailViewModel> EditContaPoupanca(ContaPoupancaDetailEditModel model);
        Task<ContaCartaoCreditoDetailViewModel> GetContaCartaoCreditoDetail(long id);
        Task<ContaCorrenteDetailViewModel> GetContaCorrenteDetail(long id);
        Task<ContaEspecieDetailViewModel> GetContaEspecieDetail(long id);
        Task<ContaPoupancaDetailViewModel> GetContaPoupancaDetail(long id);
        Task<List<ContaSelectViewModel>> GetSelectViewList(TipoConta? tipoConta);
        Task<List<ContaSummaryViewModel>> GetSummaryViewList();
        Task<List<CountModel<ContaSelectViewModel>>> GetWithProgramacaoSelectViewList();
        Task<ContaCartaoCreditoDetailViewModel> InsertContaCartaoCredito(ContaCartaoCreditoDetailInsertModel model);
        Task<ContaCorrenteDetailViewModel> InsertContaCorrente(ContaCorrenteDetailInsertModel model);
        Task<ContaEspecieDetailViewModel> InsertContaEspecie(ContaEspecieDetailInsertModel model);
        Task<ContaPoupancaDetailViewModel> InsertContaPoupanca(ContaPoupancaDetailInsertModel model);
        Task<bool> Remove(long contaId, TipoConta tipoConta);
    }
}