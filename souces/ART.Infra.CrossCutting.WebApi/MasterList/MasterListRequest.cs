namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    using System.Collections.Generic;

    public class MasterListRequest
    {
        #region Properties

        public List<IMasterListFilterColumn> FilterColumns
        {
            get; set;
        }

        public bool HasFilterColumns
        {
            get
            {
                return FilterColumns != null && FilterColumns.Count > 0;
            }
        }

        public bool HasSortColumns
        {
            get
            {
                return SortColumns != null && SortColumns.Count > 0;
            }
        }

        public int PageNumber
        {
            get; set;
        }

        public int PageSize
        {
            get; set;
        }

        public string Search
        {
            get; set;
        }

        public int Skip
        {
            get
            {
                return (PageNumber - 1) * PageSize;
            }
        }

        public List<IMasterListSortColumn> SortColumns
        {
            get; set;
        }

        #endregion Properties
    }
}