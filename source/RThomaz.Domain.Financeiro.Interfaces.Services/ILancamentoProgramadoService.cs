using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ILancamentoProgramadoService : IServiceBase
    {
        Task<LancamentoProgramadoDetailViewDTO> Edit(LancamentoProgramadoEditDTO dto);
        Task<LancamentoProgramadoDetailViewDTO> GetDetail(long id);
        Task<PagedListResponse<ProgramacaoMasterDTO>> GetMasterList(PagedListRequest<ProgramacaoMasterDTO> pagedListRequest, long? contaId, TipoConta? tipoConta);
        Task<LancamentoProgramadoDetailViewDTO> Insert(LancamentoProgramadoInsertDTO dto);
        Task<bool> Remove(long id);
    }
}