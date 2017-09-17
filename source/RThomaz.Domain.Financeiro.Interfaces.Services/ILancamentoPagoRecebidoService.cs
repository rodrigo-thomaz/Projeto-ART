using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ILancamentoPagoRecebidoService : IServiceBase
    {
        Task<LancamentoPagoRecebidoDetailViewDTO> Edit(LancamentoPagoRecebidoDetailEditDTO dto);
        Task<LancamentoPagoRecebidoDetailViewDTO> GetDetail(long lancamentoId);
        Task<LancamentoPagoRecebidoMasterListDTO> GetMasterList(PagedListRequest<LancamentoPagoRecebidoMasterDTO> pagedListRequest, MesAnoDTO periodo, long contaId, TipoConta tipoConta);
        Task<List<MesAnoDTO>> GetPeriodos(long contaId, TipoConta tipoConta);
        Task<bool> Remove(long lancamentoId, TipoTransacao tipoTransacao);
    }
}