using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IPlanoContaService : IServiceBase
    {
        Task<PlanoContaDetailViewDTO> Edit(PlanoContaDetailEditDTO dto);
        Task<PlanoContaDetailViewDTO> GetDetail(long planoContaId, TipoTransacao tipoTransacao);
        Task<List<PlanoContaMasterDTO>> GetMasterList(TipoTransacao tipoTransacao, string search, bool mostrarInativos);
        Task<SelectListResponseDTO<PlanoContaSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest, TipoTransacao tipoTransacao);
        Task<PlanoContaDetailViewDTO> Insert(PlanoContaDetailInsertDTO dto);
        Task Move(PlanoContaMasterMoveDTO dto);
        Task<bool> Remove(long planoContaId, TipoTransacao tipoTransacao);
        Task Rename(long planoContaId, string nome);
        Task<bool> UniqueNome(TipoTransacao tipoTransacao, string nome);
    }
}