using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Infra.CrossCutting.Storage;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IBancoService : IServiceBase
    {
        Task<BancoDetailViewDTO> Edit(BancoDetailEditDTO dto);
        Task<BancoDetailViewDTO> GetDetail(long bancoId);
        Task<StorageDownloadDTO> GetLogo(string storageObject);
        Task<BancoMascarasDetailViewDTO> GetMascaras(long bancoId);
        Task<PagedListResponse<BancoMasterDTO>> GetMasterList(PagedListRequest<BancoMasterDTO> pagedListRequest, bool? ativo);
        Task<SelectListResponseDTO<BancoSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest);
        Task<BancoDetailViewDTO> Insert(BancoDetailInsertDTO dto);
        Task<bool> Remove(long bancoId);
        Task<bool> UniqueNome(long? bancoId, string nome);
        Task<bool> UniqueNomeAbreviado(long? bancoId, string nomeAbreviado);
        Task<bool> UniqueNumero(long? bancoId, string numero);
    }
}