using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ILancamentoPagarReceberService : IServiceBase
    {
        Task<LancamentoPagarReceberDetailViewDTO> Edit(LancamentoPagarReceberDetailEditDTO dto);
        Task<LancamentoPagarReceberDetailViewDTO> GetDetail(long lancamentoId);
        Task<LancamentoPagarReceberMasterListDTO> GetMasterList(PagedListRequest<LancamentoPagarReceberMasterDTO> pagedListRequest, MesAnoDTO periodo, long contaId, TipoConta tipoConta);
        Task<List<MesAnoDTO>> GetPeriodos(long contaId, TipoConta tipoConta);
        Task<LancamentoPagarReceberDetailViewDTO> Insert(LancamentoPagarReceberDetailInsertDTO dto);
        Task<bool> Remove(long lancamentoId, TipoTransacao tipoTransacao);
    }
}