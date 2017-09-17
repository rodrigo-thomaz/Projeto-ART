using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Enums;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IMovimentoService : IServiceBase
    {
        Task<MovimentoDetailViewDTO> EditConciliado(MovimentoConciliadoEditDTO dto);
        Task<MovimentoDetailViewDTO> EditImportado(MovimentoImportadoEditDTO dto);
        Task<MovimentoDetailViewDTO> EditManual(MovimentoManualEditDTO dto);
        Task<MovimentoDetailViewDTO> GetDetail(long movimentoId);
        Task<MovimentoMasterListDTO> GetMasterList(PagedListRequest<MovimentoMasterDTO> pagedListRequest, MesAnoDTO periodo, long contaId, TipoConta tipoConta, TipoTransacao? tipoTransacao, ConciliacaoStatus? conciliacaoStatus, ConciliacaoOrigem? conciliacaoOrigem);
        Task<List<MesAnoDTO>> GetPeriodos(long contaId, TipoConta tipoConta);
        Task<SelectListResponseDTO<MovimentoSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest, TipoTransacao tipoTransacao);
        Task<MovimentoDetailViewDTO> Insert(MovimentoDetailInsertDTO dto);
        Task<bool> Remove(long movimentoId);
    }
}