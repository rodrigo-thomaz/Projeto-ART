using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Infra.CrossCutting.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IBancoAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<BancoMasterModel>>> GetMasterList(jQueryDataTableParameter param, bool? ativo);
        Task<BancoDetailViewModel> Edit(BancoDetailEditModel model);
        Task<BancoDetailViewModel> GetDetail(long bancoId);
        Task<StorageDownloadDTO> GetLogo(string storageObject);
        Task<BancoMascarasDetailViewModel> GetMascaras(long bancoId);
        Task<SelectListResponseModel<BancoSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest);
        Task<BancoDetailViewModel> Insert(BancoDetailInsertModel model);
        Task<bool> Remove(long bancoId);
        Task<bool> UniqueNome(long? bancoId, string nome);
        Task<bool> UniqueNomeAbreviado(long? bancoId, string nomeAbreviado);
        Task<bool> UniqueNumero(long? bancoId, string numero);
    }
}