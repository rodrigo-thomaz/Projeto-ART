using RThomaz.Application.Financeiro.Helpers.JsTree;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ICentroCustoAppService : IAppServiceBase
    {
        Task<List<JsTreeNode>> GetMasterList(string search, bool mostrarInativos);
        Task<CentroCustoDetailViewModel> Edit(CentroCustoDetailEditModel model);
        Task<CentroCustoDetailViewModel> GetDetail(long centroCustoId);
        Task<SelectListResponseModel<CentroCustoSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest);
        Task<CentroCustoDetailViewModel> Insert(CentroCustoDetailInsertModel model);
        Task Move(CentroCustoMasterMoveModel model);
        Task<bool> Remove(long centroCustoId);
        Task Rename(long centroCustoId, string nome);
        Task<bool> UniqueNome(long? centroCustoId, string nome);
    }
}