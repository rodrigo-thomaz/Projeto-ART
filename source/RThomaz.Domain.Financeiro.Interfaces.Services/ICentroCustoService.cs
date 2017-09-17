using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ICentroCustoService : IServiceBase
    {
        Task<CentroCustoDetailViewDTO> Edit(CentroCustoDetailEditDTO dto);
        Task<CentroCustoDetailViewDTO> GetDetail(long centroCustoId);
        Task<List<CentroCustoMasterDTO>> GetMasterList(string search, bool mostrarInativos);
        Task<SelectListResponseDTO<CentroCustoSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest);
        Task<CentroCustoDetailViewDTO> Insert(CentroCustoDetailInsertDTO dto);
        Task Move(CentroCustoMasterMoveDTO dto);
        Task<bool> Remove(long centroCustoId);
        Task Rename(long centroCustoId, string nome);
        Task<bool> UniqueNome(long? centroCustoId, string nome);
    }
}