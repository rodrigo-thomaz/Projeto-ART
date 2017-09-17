using RThomaz.Application.Financeiro.Helpers.Storage;

namespace RThomaz.Application.Financeiro.Models
{
    public class BandeiraCartaoDetailEditModel
    {
        public long BandeiraCartaoId { get; set; }

        public string Nome { get; set; }

        public string LogoStorageObject { get; set; }

        public StorageUploadModel StorageUpload { get; set; }

        public bool Ativo { get; set; }
    }
}