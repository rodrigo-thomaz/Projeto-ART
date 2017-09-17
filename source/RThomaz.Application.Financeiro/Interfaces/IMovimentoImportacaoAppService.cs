using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IMovimentoImportacaoAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<MovimentoImportacaoMasterModel>>> GetMasterList(jQueryDataTableParameter param, long movimentoImportacaoId, TipoTransacao? tipoTransacao);
        Task<SelectListResponseModel<MovimentoImportacaoSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest);
        Task<bool> Remove(long movimentoImportacaoId);
    }
}