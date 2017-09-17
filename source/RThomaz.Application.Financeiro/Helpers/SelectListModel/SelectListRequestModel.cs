using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Helpers.SelectListModel
{
    public class SelectListRequestModel
    {
        /// <summary>
        /// Número da página
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int PageNumber { get; set; }

        /// <summary>
        /// Número de registros por página
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }

        /// <summary>
        /// Filtro de busca
        /// </summary>
        public string Search { get; set; }
    }
}