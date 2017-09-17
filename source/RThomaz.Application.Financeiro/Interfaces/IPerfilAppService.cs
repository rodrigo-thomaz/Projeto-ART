using RThomaz.Application.Financeiro.Models;
using RThomaz.Infra.CrossCutting.Storage;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IPerfilAppService : IAppServiceBase
    {
        Task<string> EditAvatar(PerfilAvatarEditModel model);
        Task<PerfilPersonalInfoViewModel> EditPersonalInfo(PerfilPersonalInfoEditModel model);
        Task<StorageDownloadDTO> GetAvatar(string storageObject);
        Task<PerfilPersonalInfoViewModel> GetDetail(long usuarioId);
    }
}