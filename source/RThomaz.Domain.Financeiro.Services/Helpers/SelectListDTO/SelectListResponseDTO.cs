using System.Collections.Generic;

namespace RThomaz.Business.Helpers.SelectListDTO
{
    public class SelectListDTOResponse<T>
    {
        private readonly List<T> _data;
        private readonly int _totalRecords;

        public SelectListDTOResponse(List<T> data, int totalRecords)
        {
            _data = data;
            _totalRecords = totalRecords;
        }

        public List<T> Data { get { return _data; } }

        public int TotalRecords { get { return _totalRecords; } }
    }
}
