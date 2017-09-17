using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ITransferenciaAppService : IAppServiceBase
    {
        Task<TransferenciaDetailViewModel> Edit(TransferenciaDetailEditModel model);
        Task<TransferenciaDetailViewModel> GetDetail(long transferenciaId);
        Task<TransferenciaDetailViewModel> Insert(TransferenciaDetailInsertModel model);
        Task<bool> Remove(long transferenciaId);
    }
}