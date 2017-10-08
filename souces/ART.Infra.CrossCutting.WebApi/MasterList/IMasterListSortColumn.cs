using ART.Infra.CrossCutting.WebApi.MasterList;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public interface IMasterListSortColumn
    {
        string ColumnName { get; }
        MasterListSortDirection SortDirection { get; }
        int Priority { get; }
    }
}
