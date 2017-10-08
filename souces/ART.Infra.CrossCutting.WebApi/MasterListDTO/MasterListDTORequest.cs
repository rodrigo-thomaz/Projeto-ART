using System.Collections.Generic;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public class MasterListDTORequest<TSource>
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;
        private readonly string _search;
        private readonly List<IMasterListDTOFilterColumn> _filterColumns;
        private readonly List<IMasterListDTOSortColumn> _sortColumns;

        public MasterListDTORequest(int pageNumber, int pageSize, string search, List<IMasterListDTOFilterColumn> filterColumns, List<IMasterListDTOSortColumn> sortColumns)
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
        public List<IMasterListDTOFilterColumn> FilterColumns { get { return _filterColumns; } }
        public List<IMasterListDTOSortColumn> SortColumns { get { return _sortColumns; } }
    }
}
