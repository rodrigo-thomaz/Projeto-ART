using System.Collections.Generic;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }        
        public string Search { get; set; }
        public List<MasterListFilterColumn> FilterColumns { get; set; }
        public List<MasterListSortColumn> SortColumns { get; set; }

        public int Skip
        {
            get
            {
                return (PageNumber - 1) * PageSize;
            }
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
    }
}