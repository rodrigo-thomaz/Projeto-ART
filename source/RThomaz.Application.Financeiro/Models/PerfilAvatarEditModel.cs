using RThomaz.Application.Financeiro.Helpers.Storage;

namespace RThomaz.Application.Financeiro.Models
{
    public class PerfilAvatarEditModel
    {
        public long UsuarioId { get; set; }

        public StorageUploadModel StorageUpload { get; set; }
        public string AvatarStorageObject { get; set; }
    }
}