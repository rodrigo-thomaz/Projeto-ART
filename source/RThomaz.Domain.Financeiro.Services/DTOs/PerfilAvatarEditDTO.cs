using RThomaz.Business.Helpers.Storage;

namespace RThomaz.Business.DTOs
{
    public class PerfilChangeAvatarDTO
    {
        private readonly long _usuarioId;
        private readonly StorageUploadDTO _arquivo;
        private readonly string _avatarStorageObject;

        public PerfilChangeAvatarDTO
            (
                  long usuarioId
                , StorageUploadDTO arquivo
                , string avatarStorageObject
            )
        {
            _usuarioId = usuarioId;
            _arquivo = arquivo;
            _avatarStorageObject = avatarStorageObject;
        }       

        public long UsuarioId { get { return _usuarioId; } }
        public StorageUploadDTO Arquivo { get { return _arquivo; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
    }
}
