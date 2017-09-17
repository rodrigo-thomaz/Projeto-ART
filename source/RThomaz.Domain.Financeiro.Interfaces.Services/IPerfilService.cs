using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.CrossCutting.Storage;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IPerfilService : IServiceBase
    {
        Task<string> EditAvatar(PerfilAvatarEditDTO dto);
        Task<PerfilPersonalInfoViewDTO> EditPersonalInfo(PerfilPersonalInfoEditDTO dto);
        Task<StorageDownloadDTO> GetAvatar(string storageObject);
        Task<PerfilPersonalInfoViewDTO> GetDetail(long usuarioId);
    }
}