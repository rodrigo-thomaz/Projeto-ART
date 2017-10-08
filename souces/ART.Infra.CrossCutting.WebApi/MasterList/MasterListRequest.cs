namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }        
        public string Search { get; set; }
        public MasterListFilterColumn[] FilterColumns { get; set; }
        public MasterListSortColumn[] SortColumns { get; set; }
    }
}