using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList
{
    public class PagedListResponse<T>
    {
        private readonly List<T> _data;
        private readonly int _totalRecords;

        public PagedListResponse(List<T> data, int totalRecords)
        {
            _data = data;
            _totalRecords = totalRecords;
        }

        public List<T> Data { get { return _data; } }

        public int TotalRecords { get { return _totalRecords; } }
    }
}
