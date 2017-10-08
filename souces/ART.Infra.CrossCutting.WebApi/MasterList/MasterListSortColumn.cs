namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public class MasterListSortColumn : IMasterListSortColumn
    {
        public string ColumnName { get; set; }
        public MasterListSortDirection SortDirection { get; set; }        
        public int Priority { get; set; }
    }
}
