using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IMovimentoImportacaoService : IServiceBase
    {
        Task<PagedListResponse<MovimentoImportacaoMasterDTO>> GetMasterList(PagedListRequest<MovimentoImportacaoMasterDTO> pagedListRequest, long movimentoImportacaoId, TipoTransacao? tipoTransacao);
        Task<SelectListResponseDTO<MovimentoImportacaoSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest);
        Task<bool> Remove(long movimentoImportacaoId);
    }
}