using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IUsuarioAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<UsuarioMasterModel>>> GetMasterList(jQueryDataTableParameter param, bool? ativo);
        Task<UsuarioDetailViewModel> Edit(UsuarioDetailEditModel model);
        Task<UsuarioDetailViewModel> GetDetail(long usuarioId);
        Task<SelectListResponseModel<UsuarioSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest);
        Task<UsuarioDetailViewModel> Insert(UsuarioDetailInsertModel model);
        Task<bool> Remove(long usuarioId);
        Task<bool> UniqueEmail(string email);
    }
}