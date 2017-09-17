using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Application.Financeiro.Helpers.SelectListModel
{
    public class SelectListResponseModel<TSelectModel>
    {
        #region private fields

        private readonly int _totalRecords;
        private readonly List<TSelectModel> _data;

        #endregion

        #region constructors

        public SelectListResponseModel(int totalRecords, List<TSelectModel> data)
        {
            _totalRecords = totalRecords;
            _data = data;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Número total de registros
        /// </summary>
        [Required]
        public int TotalRecords { get { return _totalRecords; } }

        /// <summary>
        /// Registros da página
        /// </summary>
        [Required]
        public List<TSelectModel> Data { get { return _data; } }

        #endregion
    }
}