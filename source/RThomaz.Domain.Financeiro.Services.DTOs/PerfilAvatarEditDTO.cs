using RThomaz.Infra.CrossCutting.Storage;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PerfilAvatarEditDTO
    {
        private readonly long _usuarioId;
        private readonly StorageUploadDTO _storageUpload;
        private readonly string _avatarStorageObject;

        public PerfilAvatarEditDTO
            (
                  long usuarioId
                , StorageUploadDTO storageUpload
                , string avatarStorageObject
            )
        {
            _usuarioId = usuarioId;
            _storageUpload = storageUpload;
            _avatarStorageObject = avatarStorageObject;
        }       

        public long UsuarioId { get { return _usuarioId; } }
        public StorageUploadDTO StorageUpload { get { return _storageUpload; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
    }
}
