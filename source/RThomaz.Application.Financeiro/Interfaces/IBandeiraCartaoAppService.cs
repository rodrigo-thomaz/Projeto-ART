using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Infra.CrossCutting.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IBandeiraCartaoAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<BandeiraCartaoMasterModel>>> GetMasterList(jQueryDataTableParameter param, bool? ativo);
        Task<BandeiraCartaoDetailViewModel> Edit(BandeiraCartaoDetailEditModel model);
        Task<BandeiraCartaoDetailViewModel> GetDetail(long id);
        Task<StorageDownloadDTO> GetLogo(string storageObject);
        Task<List<BandeiraCartaoSelectViewModel>> GetSelectViewList();
        Task<BandeiraCartaoDetailViewModel> Insert(BandeiraCartaoDetailInsertModel model);
        Task<bool> Remove(long id);
        Task<bool> UniqueNome(long? bandeiraCartaoId, string nome);
    }
}