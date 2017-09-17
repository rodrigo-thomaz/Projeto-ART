using RThomaz.Application.Financeiro.Helpers.Storage;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Models
{
    public class BancoDetailEditModel
    {
        /// <summary>
        /// Id do banco
        /// </summary>
        [Required]
        public long BancoId { get; set; }

        /// <summary>
        /// Nome do banco
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Nome abreviado do banco
        /// </summary>
        [Required]
        public string NomeAbreviado { get; set; }

        /// <summary>
        /// Número do banco
        /// </summary>
        [Required]
        public string Numero { get; set; }

        /// <summary>
        /// Máscara do número das agências do banco
        /// </summary>
        public string MascaraNumeroAgencia { get; set; }

        /// <summary>
        /// Máscara do número das contas corrente do banco
        /// </summary>
        public string MascaraNumeroContaCorrente { get; set; }

        /// <summary>
        /// Máscara do número das contas poupança do banco
        /// </summary>
        public string MascaraNumeroContaPoupanca { get; set; }

        /// <summary>
        /// Código para importação de extrato no formato Ofx
        /// </summary>
        public string CodigoImportacaoOfx { get; set; }

        /// <summary>
        /// Site do banco
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Descrição do banco
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Status de ativo do banco
        /// </summary>
        [Required]
        public bool Ativo { get; set; }

        /// <summary>
        /// Upload de um novo logo no storage
        /// </summary>
        public StorageUploadModel StorageUpload { get; set; }

        /// <summary>
        /// Nome do logo no storage
        /// </summary>
        public string LogoStorageObject { get; set; }
    }
}