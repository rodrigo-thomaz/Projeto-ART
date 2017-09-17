using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase
    {
        Task<UsuarioDetailViewDTO> Edit(UsuarioDetailEditDTO dto);
        Task<UsuarioDetailViewDTO> GetDetail(long usuarioId);
        Task<PagedListResponse<UsuarioMasterDTO>> GetMasterList(PagedListRequest<UsuarioMasterDTO> pagedListRequest, bool? ativo);
        Task<SelectListResponseDTO<UsuarioSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest);
        Task<UsuarioDetailViewDTO> Insert(UsuarioDetailInsertDTO dto);
        Task<bool> Remove(long usuarioId);
        Task<bool> UniqueEmail(string email);
    }
}