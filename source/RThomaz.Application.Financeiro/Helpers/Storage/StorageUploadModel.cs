using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Helpers.Storage
{
    public class StorageUploadModel
    {
        /// <summary>
        /// String buffer Base64 do arquivo
        /// </summary>
        [Required]
        public string BufferBase64String { get; set; }

        /// <summary>
        /// O ContentType do arquivo
        /// </summary>
        [Required]
        public string ContentType { get; set; }
    }
}