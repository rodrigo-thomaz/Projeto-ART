using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Infra.CrossCutting.Storage;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IBandeiraCartaoService : IServiceBase
    {
        Task<BandeiraCartaoDetailViewDTO> Edit(BandeiraCartaoDetailEditDTO dto);
        Task<BandeiraCartaoDetailViewDTO> GetDetail(long id);
        Task<StorageDownloadDTO> GetLogo(string storageObject);
        Task<PagedListResponse<BandeiraCartaoMasterDTO>> GetMasterList(PagedListRequest<BandeiraCartaoMasterDTO> pagedListRequest, bool? ativo);
        Task<List<BandeiraCartaoSelectViewDTO>> GetSelectViewList();
        Task<BandeiraCartaoDetailViewDTO> Insert(BandeiraCartaoDetailInsertDTO dto);
        Task<bool> Remove(long id);
        Task<bool> UniqueNome(long? bandeiraCartaoId, string nome);
    }
}