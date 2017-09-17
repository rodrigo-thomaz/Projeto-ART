using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ITransferenciaProgramadaService : IServiceBase
    {
        Task<TransferenciaProgramadaDetailViewDTO> Edit(TransferenciaProgramadaDetailEditDTO dto);
        Task<TransferenciaProgramadaDetailViewDTO> GetDetail(long id);
        Task<TransferenciaProgramadaDetailViewDTO> Insert(TransferenciaProgramadaDetailInsertDTO dto);
        Task<bool> Remove(long programacaoId);
    }
}