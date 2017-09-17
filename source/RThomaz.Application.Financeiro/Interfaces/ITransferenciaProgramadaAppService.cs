using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ITransferenciaProgramadaAppService : IAppServiceBase
    {
        Task<TransferenciaProgramadaDetailViewModel> Edit(TransferenciaProgramadaDetailEditModel model);
        Task<TransferenciaProgramadaDetailViewModel> GetDetail(long id);
        Task<TransferenciaProgramadaDetailViewModel> Insert(TransferenciaProgramadaDetailInsertModel model);
        Task<bool> Remove(long programacaoId);
    }
}