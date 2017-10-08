using System.Collections.Generic;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public class MasterListDTOResponse<T>
    {
        private readonly List<T> _data;
        private readonly int _totalRecords;

        public MasterListDTOResponse(List<T> data, int totalRecords)
        {
            _data = data;
            _totalRecords = totalRecords;
        }

        public List<T> Data { get { return _data; } }

        public int TotalRecords { get { return _totalRecords; } }
    }
}
