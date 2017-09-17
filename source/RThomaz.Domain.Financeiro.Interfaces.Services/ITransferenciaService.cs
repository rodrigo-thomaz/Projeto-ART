using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ITransferenciaService : IServiceBase
    {
        Task<TransferenciaDetailViewDTO> Edit(TransferenciaDetailEditDTO dto);
        Task<TransferenciaDetailViewDTO> GetDetail(long transferenciaId);
        Task<TransferenciaDetailViewDTO> Insert(TransferenciaDetailInsertDTO dto);
        Task<bool> Remove(long transferenciaId);
    }
}