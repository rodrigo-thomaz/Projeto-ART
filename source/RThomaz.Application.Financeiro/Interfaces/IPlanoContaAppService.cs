using RThomaz.Application.Financeiro.Helpers.JsTree;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IPlanoContaAppService : IAppServiceBase
    {
        Task<List<JsTreeNode>> GetMasterList(TipoTransacao tipoTransacao, string search, bool mostrarInativos);
        Task<PlanoContaDetailViewModel> Edit(PlanoContaDetailEditModel model);
        Task<PlanoContaDetailViewModel> GetDetail(long planoContaId, TipoTransacao tipoTransacao);
        Task<SelectListResponseModel<PlanoContaSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest, TipoTransacao tipoTransacao);
        Task<PlanoContaDetailViewModel> Insert(PlanoContaDetailInsertModel model);
        Task Move(PlanoContaMasterMoveModel model);
        Task<bool> Remove(long planoContaId, TipoTransacao tipoTransacao);
        Task Rename(long planoContaId, string nome);
        Task<bool> UniqueNome(TipoTransacao tipoTransacao, string nome);
    }
}