using RThomaz.Application.Financeiro.Helpers.Storage;

namespace RThomaz.Application.Financeiro.Models
{
    public class BandeiraCartaoDetailInsertModel
    {
        public string Nome { get; set; }
                
        public StorageUploadModel StorageUpload { get; set; }

        public bool Ativo { get; set; }
    }
}