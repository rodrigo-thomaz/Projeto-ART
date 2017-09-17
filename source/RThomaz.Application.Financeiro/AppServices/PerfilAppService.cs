using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.Storage;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Helpers.Storage;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class PerfilAppService : AppServiceBase, IPerfilAppService
    {
        #region private fields

        private readonly IPerfilService _perfilService;

        #endregion

        #region constructors

        public PerfilAppService(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        #endregion

        #region public voids

        public async Task<string> EditAvatar(PerfilAvatarEditModel model)
        {
            var storageUploadDTO = Mapper.Map<StorageUploadModel, StorageUploadDTO>(model.StorageUpload);
            var editDTO = Mapper.Map<PerfilAvatarEditModel, PerfilAvatarEditDTO>(model);
            var storageObject = await _perfilService.EditAvatar(editDTO);
            return storageObject;
        }

        public async Task<PerfilPersonalInfoViewModel> EditPersonalInfo(PerfilPersonalInfoEditModel model)
        {
            var editDTO = Mapper.Map<PerfilPersonalInfoEditModel, PerfilPersonalInfoEditDTO>(model);
            var viewDTO = await _perfilService.EditPersonalInfo(editDTO);
            var viewModel = Mapper.Map<PerfilPersonalInfoViewDTO, PerfilPersonalInfoViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<StorageDownloadDTO> GetAvatar(string storageObject)
        {
            var result = await _perfilService.GetAvatar(storageObject);            
            return result;
        }

        public async Task<PerfilPersonalInfoViewModel> GetDetail(long usuarioId)
        {
            var dto = await _perfilService.GetDetail(usuarioId);
            var model = Mapper.Map<PerfilPersonalInfoViewDTO, PerfilPersonalInfoViewModel>(dto);
            return model;
        }

        #endregion
    }
}