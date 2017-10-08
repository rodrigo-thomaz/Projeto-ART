using ART.Infra.CrossCutting.WebApi.MasterList;
using System.Collections.Generic;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public class MasterListDTORequest
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;
        private readonly string _search;
        private readonly List<IMasterListFilterColumn> _filterColumns;
        private readonly List<IMasterListSortColumn> _sortColumns;

        public MasterListDTORequest(int pageNumber, int pageSize, string search, List<IMasterListFilterColumn> filterColumns, List<IMasterListSortColumn> sortColumns)
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
            _search = search;
            _filterColumns = filterColumns;
            _sortColumns = sortColumns;
        }

        public int Skip
        {
            get
            {
                return (_pageNumber - 1) * _pageSize;
            }
        }

        public bool HasFilterColumns
        {
            get
            {
                return _filterColumns != null && _filterColumns.Count > 0;
            }
        }

        public bool HasSortColumns
        {
            get
            {
                return _sortColumns != null && _sortColumns.Count > 0;
            }
        }

        public int PageNumber { get { return _pageNumber; } }
        public int PageSize { get { return _pageSize; } }
        public string Search { get { return _search; } }
        public List<IMasterListFilterColumn> FilterColumns { get { return _filterColumns; } }
        public List<IMasterListSortColumn> SortColumns { get { return _sortColumns; } }
    }
}
